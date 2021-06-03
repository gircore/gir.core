namespace GirLoader.Output.Model
{
    public class Pointer : PrimitiveType
    {
        public Pointer(string ctypeName) : base(new CType(ctypeName), new SymbolName("IntPtr")) { }
        
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
