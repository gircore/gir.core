namespace GirLoader.Output;

internal class ReturnValueFactory(TypeReferenceFactory typeReferenceFactory, TransferFactory transferFactory)
{
    public ReturnValue Create(Input.ReturnValue returnValue)
    {
        return new ReturnValue(
            anyTypeReference: typeReferenceFactory.CreateAnyTypeReference(returnValue),
            transfer: transferFactory.FromText(returnValue.TransferOwnership),
            nullable: returnValue.Nullable
        );
    }

    public ReturnValue Create(string ctype, Transfer transfer, bool nullable)
    {
        return new ReturnValue(
            anyTypeReference: typeReferenceFactory.CreateTypeReference(ctype, ctype),
            transfer: transfer,
            nullable: nullable
        );
    }
}
