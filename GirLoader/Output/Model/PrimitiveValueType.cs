#nullable enable
namespace GirLoader.Output.Model
{
    public abstract class PrimitiveValueType : PrimitiveType
    {
        protected PrimitiveValueType(CType ctype, SymbolName symbolName) : base(ctype, symbolName)
        {
        }

        internal override bool Matches(TypeReference typeReference)
        {
            if (typeReference.CTypeReference is not null)
                return typeReference.CTypeReference.CType == CType;

            if (typeReference.OriginalName.Value is not null)
                return typeReference.OriginalName.Value == CType.Value;
            
            return false;
        }
    }
}
