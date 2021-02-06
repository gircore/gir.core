namespace Repository.Model
{
    /// <summary>
    /// A type is considered to be any instantiable type
    /// This includes Objects, Interfaces, Delegates, Enums, etc
    /// </summary>
    public interface IType : ISymbol
    {
        Namespace Namespace { get; }
       
    }

    public interface ISymbol
    {
        string NativeName { get; }
        string? ManagedName { get; }
    }

    public class Symbol : ISymbol
    {
        public string NativeName { get; }
        public string? ManagedName { get; }

        public Symbol(string nativeName, string managedName)
        {
            NativeName = nativeName;
            ManagedName = managedName;
        }
    }

    public abstract class Type : Symbol, IType
    {
        public Namespace Namespace { get; }

        protected Type(Namespace @namespace, string nativeName, string managedName) : base(nativeName, managedName)
        {
            Namespace = @namespace;
        }
    }
}
