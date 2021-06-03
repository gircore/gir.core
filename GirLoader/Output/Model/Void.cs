namespace GirLoader.Output.Model
{
    public class Void : PrimitiveType
    {
        public Void(string nativeName) : base(new CType(nativeName), new SymbolName("void")) { }
        
        internal override bool Matches(TypeReference typeReference)
        {
            if (!SameNamespace(typeReference))
                return false;
            
            return typeReference.CType?.Value == CType.Value;
        }
    }
}
