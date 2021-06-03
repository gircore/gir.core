#nullable enable
namespace GirLoader.Output.Model
{
    public abstract class PrimitiveValueType : PrimitiveType
    {
        protected PrimitiveValueType(CType cType, SymbolName symbolName) : base(cType, symbolName)
        {
        }

        internal override bool Matches(TypeReference typeReference)
        {
            if (!SameNamespace(typeReference))
                return false;

            if (typeReference.CType is null)
                return false;
            
            return typeReference.CType.Value == CType.Value && !typeReference.CType.IsPointer;
        }
    }
}
