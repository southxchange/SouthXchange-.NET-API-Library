namespace SouthXchange.Model
{
    public enum WalletStatus
    {
        NoInfo = 0,
        NoConnections = 1,
        Good = 2,
        LowConnections = 3,
        OutOfSync = 4,
        Maintenance = 5,
        Disabled = 6,
    }
}
