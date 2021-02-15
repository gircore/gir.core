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

    public abstract class Type : Symbol, IType
    {
        public Namespace Namespace { get; }

        protected Type(Namespace @namespace, string nativeName, string managedName) : base(nativeName, managedName)
        {
            Namespace = @namespace;
        }
    }
}
