namespace Repository.Model
{
    public record Interface : IType
    {
        public Namespace Namespace { get; init; }
        public string ManagedName { get; set; }
        public string NativeName { get; init; }
    }
}
