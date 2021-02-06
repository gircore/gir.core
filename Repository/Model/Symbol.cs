namespace Repository.Model
{
    public interface ISymbol
    {
        string NativeName { get; }
        string ManagedName { get; }
    }

    public class Symbol : ISymbol
    {
        public string NativeName { get; }
        public string ManagedName { get; }

        public Symbol(string nativeName, string managedName)
        {
            NativeName = nativeName;
            ManagedName = managedName;
        }
    }
}
