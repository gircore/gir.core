namespace GirModel
{
    public enum Transfer
    {
        None = 0,
        Container = 1,
        Full = 2
    }

    public static class TransferExtension
    {
        public static bool IsOwnedRef(this Transfer transfer) => transfer switch
        {
            Transfer.None => false,
            Transfer.Full => true,
            Transfer.Container => true,
            _ => throw new System.Exception("Unknown transfer type")
        };
    }
}
