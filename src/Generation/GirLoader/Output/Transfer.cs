namespace GirLoader.Output
{
    public enum Transfer
    {
        Unknown,
        None,
        Container,
        Full
    }
    
    internal static class TransferConverter
    {
        public static GirModel.Transfer ToGirModel(this Transfer transfer) => transfer switch
        {
            Transfer.Container => GirModel.Transfer.Container,
            Transfer.Full => GirModel.Transfer.Full,
            Transfer.None => GirModel.Transfer.None,
            _ => GirModel.Transfer.None
        };
    }
}
