#nullable enable

namespace Repository.Model
{
    // A symbol is considered to be any instantiable type
    // This includes Objects, Interfaces, Delegates, Enums, etc
    public interface IType
    {
        Namespace? Namespace { get; init; }
        string NativeName { get; init; }
        string ManagedName { get; set; } //TODO: Should not be setable. Records are changed via "with". If this is not desired the types should be classes instead of records.
    }

    public record BasicType : IType
    {
        // TODO: Override init accessors here
        public Namespace? Namespace { get; init; }
        public string NativeName { get; init; }
        public string ManagedName { get; set; }

        public BasicType(string from, string to)
        {
            NativeName = from;
            ManagedName = to;
        }
    }
}
