namespace GirLoader.Output;

public class Void : Type, GirModel.Void
{
    public Void(string ctype) : base(ctype) { }

    internal override bool Matches(TypeReference typeReference)
    {
        if (typeReference.CTypeReference is null)
            return false;

        return typeReference.CTypeReference.CType == CType
               && !typeReference.CTypeReference.IsPointer;
    }
}
