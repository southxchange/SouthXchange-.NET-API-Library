using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SouthXchange.Model.Messages;
using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SouthXchange
{
    public class RealTimeContext
    {
        #region Private Attributes

        private const int ReceiveChunkSize = 512;
        private ClientWebSocket webSocket;
        private Uri baseUri;
        private BlockingCollection<dynamic> messageQueue = new BlockingCollection<dynamic>();

        #endregion

        #region Events

        public class EventArgs<T> : EventArgs
        {
            public T[] Data { get; set; }
        }

        public delegate void EventHandler<T>(object sender, EventArgs<T> e);

        public event EventHandler OnConnected;
        public event EventHandler OnDisconnected;
        public event EventHandler<MarketBook> OnMarketBook;
        public event EventHandler<BookDeltaItem> OnBookDeltaItem;
        public event EventHandler<Trade> OnTrade;
        public event EventHandler<Order> OnOrder;
        public event EventHandler<Order> OnAllOrders;

        #endregion

        #region Enums 

        public enum Command
        {
            Request,
            Subscribe,
            Unsubscribe
        }

        #endregion

        #region Constructors

        public RealTimeContext(Uri baseUri)
        {
            this.baseUri = (new UriBuilder(baseUri)
            {
                Scheme = baseUri.Scheme == Uri.UriSchemeHttps
                ? "wss" 
                : "ws"
            }).Uri;
        }

        #endregion

        #region Public Methods

        public void Connect(string token = null)
        {
            if (webSocket != null 
                && webSocket.State == WebSocketState.Open)
            {
                throw new Exception("Web socket is open.");
            }
            try
            {
                webSocket = new ClientWebSocket();
                webSocket.Options.KeepAliveInterval = TimeSpan.FromSeconds(20);
                var uri = new Uri(baseUri, string.IsNullOrEmpty(token) ? "connect" : $"connect?token={token}");
                webSocket.ConnectAsync(uri, CancellationToken.None).GetAwaiter().GetResult();
                OnConnected?.Invoke(this, new EventArgs());
                StartSending(webSocket);
                StartReceiving(webSocket);
            }
            catch (WebSocketException)
            {
                OnDisconnected?.Invoke(this, new EventArgs());
            }
        }

        public void Disconnect()
        {
            Disconnect(webSocket);
        }

        public void Request(long marketId)
        {
            Send(Command.Request, marketId);
        }

        public void Subscribe(long marketId)
        {
            Send(Command.Subscribe, marketId);
        }

        public void Unsubscribe(long marketId)
        {
            Send(Command.Unsubscribe, marketId);
        }

        public void Send(Command command, object value)
        {
            messageQueue.Add(new
            {
                k = command.ToString().ToLower(),
                v = value
            });
        }

        #endregion

        #region Private Methods

        private void Disconnect(ClientWebSocket webSocket)
        {
            if (webSocket.State == WebSocketState.Open)
            {
                lock (webSocket)
                {
                    if (webSocket.State == WebSocketState.Open)
                    {
                        try
                        {
                            webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, string.Empty, CancellationToken.None).GetAwaiter().GetResult();
                        }
                        finally
                        {
                            if (webSocket.State != WebSocketState.Open)
                            {
                                OnDisconnected?.Invoke(this, new EventArgs());
                            }
                        }
                    }
                }
            }
        }

        private void StartSending(ClientWebSocket webSocket)
        {
            new Thread(async () => 
            {
                dynamic message = null;
                try
                {
                    while (webSocket.State == WebSocketState.Open)
                    {
                        if (messageQueue.TryTake(out message, 100))
                        {
                            var encoded = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));
                            await webSocket.SendAsync(
                                new ArraySegment<Byte>(encoded, 0, encoded.Length),
                                WebSocketMessageType.Text,
                                true,
                                CancellationToken.None);
                        }
                    }
                }
                catch (WebSocketException)
                {
                    if (message != null)
                    {
                        messageQueue.Add(message);
                    }
                }
            }).Start();
        }

        private void StartReceiving(ClientWebSocket webSocket)
        {
            new Thread(async () =>
            {
                string receivedMessage;
                WebSocketReceiveResult result;
                var message = new ArraySegment<byte>(new byte[ReceiveChunkSize]);

                try
                {
                    while (webSocket.State == WebSocketState.Open)
                    {
                        receivedMessage = string.Empty;
                        do
                        {
                            result = await webSocket.ReceiveAsync(message, CancellationToken.None);
                            if (result.MessageType == WebSocketMessageType.Close)
                            {
                                Disconnect(webSocket);
                                break;
                            }
                            else
                            {
                                receivedMessage += Encoding.UTF8.GetString(message.Take(result.Count).ToArray());
                            }
                        }
                        while (!result.EndOfMessage);

                        if (string.IsNullOrEmpty(receivedMessage))
                        {
                            continue;
                        }

                        var token = JToken.Parse(receivedMessage);
                        var messageKey = token.SelectToken("k").Value<string>();
                        var messageValueToken = token.SelectToken("v");
                        switch (messageKey)
                        {
                            case "book":
                                OnMarketBook?.Invoke(this, new EventArgs<MarketBook>()
                                {
                                    Data = messageValueToken.ToObject<MarketBook[]>()
                                });
                                break;
                            case "bookdelta":
                                OnBookDeltaItem?.Invoke(this, new EventArgs<BookDeltaItem>()
                                {
                                    Data = messageValueToken.ToObject<BookDeltaItem[]>()
                                });
                                break;
                            case "trade":
                                OnTrade?.Invoke(this, new EventArgs<Trade>()
                                {
                                    Data = messageValueToken.ToObject<Trade[]>()
                                });
                                break;
                            case "allorders":
                                OnAllOrders?.Invoke(this, new EventArgs<Order>()
                                {
                                    Data = messageValueToken.ToObject<Order[]>()
                                }); 
                                break;
                            case "order":
                                OnOrder?.Invoke(this, new EventArgs<Order>()
                                {
                                    Data = messageValueToken.ToObject<Order[]>()
                                });
                                break;
                        }
                    }
                }
                catch 
                {
                    OnDisconnected?.Invoke(this, new EventArgs());
                }
                finally
                {
                    webSocket.Dispose();
                }
            }).Start();
        }

        #endregion
    }
}
