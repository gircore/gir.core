namespace Generator.Model;

internal static class Transfer
{
    public static bool IsOwnedRef(GirModel.Transfer transfer) => transfer switch
    {
        GirModel.Transfer.None => false,
        GirModel.Transfer.Full => true,
        GirModel.Transfer.Container => true,
        _ => throw new System.Exception("Unknown transfer type")
    };
}
