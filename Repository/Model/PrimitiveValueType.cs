namespace Repository.Model
{
    public class PrimitiveValueType : Symbol
    {
        public PrimitiveValueType(string nativeName, string managedName) 
            : base(new CTypeName(nativeName), new TypeName(nativeName), new SymbolName(managedName))
        {
        }
    }
}
