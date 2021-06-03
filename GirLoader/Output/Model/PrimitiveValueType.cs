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
            if (!SameNamespace(typeReference))
                return false;

            if (typeReference.CTypeReference is null)
                return false;
            
            return typeReference.CTypeReference.CType == CType && !typeReference.CTypeReference.IsPointer;
        }
    }
}
