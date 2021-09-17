using Newtonsoft.Json;

namespace SouthXchange.Model
{
    public class ListCallbacksRequest : SortedPagedRequest
    {
        public ListCallbacksRequest()
        {
            SortField = "Id";
            Descending = true;
        }
    }
}
