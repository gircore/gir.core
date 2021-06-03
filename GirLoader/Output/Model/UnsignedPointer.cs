namespace GirLoader.Output.Model
{
    public class UnsignedPointer : PrimitiveType
    {
        public UnsignedPointer(string cTypeName) : base(new CType(cTypeName), new SymbolName("UIntPtr")) { }
        
        internal override bool Matches(TypeReference typeReference)
        {
            if (!SameNamespace(typeReference))
                return false;
            
            if (typeReference.CType is null)
                return false;
            
            return typeReference.CType.Value == CType.Value;
        }
    }
}
