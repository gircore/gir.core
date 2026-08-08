namespace GirLoader.Output;

public partial class ReturnValue
{
    public Transfer Transfer { get; }
    public bool Nullable { get; }
    public AnyTypeReference AnyTypeReference { get; }

    public ReturnValue(AnyTypeReference anyTypeReference, Transfer transfer, bool nullable)
    {
        AnyTypeReference = anyTypeReference;
        Transfer = transfer;
        Nullable = nullable;
    }
}
