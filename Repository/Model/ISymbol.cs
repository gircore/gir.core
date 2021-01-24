#nullable enable

namespace Repository.Model
{
    // A symbol is considered to be any instantiable type
    // This includes Objects, Interfaces, Delegates, Enums, etc
    public interface ISymbol
    {
        Namespace? Namespace { get; init; }
        string NativeName { get; init; }
        string ManagedName { get; set; }
    }

    public record BasicSymbol : ISymbol
    {
        // TODO: Override init accessors here
        public Namespace? Namespace { get; init; }
        public string NativeName { get; init; }
        public string ManagedName { get; set; }

        public BasicSymbol(string from, string to)
        {
            NativeName = from;
            ManagedName = to;
        }
    }
}
