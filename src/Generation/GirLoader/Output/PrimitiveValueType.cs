namespace GirLoader.Output;

public abstract class PrimitiveValueType(string ctype) : Type(ctype)
{
    internal override bool Matches(TypeReference typeReference)
    {
        return typeReference switch
        {
            { SymbolNameReference: { SymbolName: { } sn } } => sn == CType,
            { CTypeReference: { } cr } => cr.CType == CType,
            _ => false
        };
    }
}
