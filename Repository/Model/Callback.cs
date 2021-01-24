namespace Repository.Model
{
    public record Callback : ISymbol
    {
        public Namespace Namespace { get; init; }
        public string ManagedName { get; set; }
        public string NativeName { get; init; }
    }
}
