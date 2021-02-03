namespace Repository.Model
{
    /// <summary>
    /// A type is considered to be any instantiable type
    /// This includes Objects, Interfaces, Delegates, Enums, etc
    /// </summary>
    public interface IType
    {
        Namespace? Namespace { get; }
        string NativeName { get; }
        string? ManagedName { get; set; }
    }

    public class BasicType : IType
    {
        public Namespace? Namespace { get; }
        public string NativeName { get; }
        public string? ManagedName { get; set; }

        public BasicType(Namespace @namespace, string nativeName, string managedName)
        {
            Namespace = @namespace;
            NativeName = nativeName;
            ManagedName = managedName;
        }
        
        public BasicType(string from, string to)
        {
            Namespace = default;
            NativeName = from;
            ManagedName = to;
        }
    }
}
