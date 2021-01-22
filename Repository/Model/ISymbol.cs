namespace Repository.Model
{
    // A symbol is considered to be any instantiable type
    // This includes Objects, Interfaces, Delegates, Enums, etc
    public interface ISymbol
    {
        string Namespace { get; }
        string NativeName { get; }
        string ManagedName { get; }
    }

    public record BasicSymbol : ISymbol
    {
        public string Namespace => string.Empty;
        public string NativeName { get; }
        public string ManagedName { get; }

        public BasicSymbol(string from, string to)
        {
            NativeName = from;
            ManagedName = to;
        }
    }
}
