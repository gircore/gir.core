namespace Repository.Model
{
    public class String : Symbol
    {
        public String(string nativeName) 
            : base(new CTypeName(nativeName), new TypeName(nativeName), new SymbolName("string")){ }
    }
}
