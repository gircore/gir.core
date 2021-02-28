namespace Repository.Model
{
    /// <summary>
    /// A type is considered to be any instantiable type
    /// This includes Objects, Interfaces, Delegates, Enums, etc
    /// </summary>
    public abstract class Type : Symbol
    {
        public Namespace Namespace { get; }

        protected Type(Namespace @namespace, string name, string managedName) : this(@namespace, name, name, managedName)
        {
        }
        
        protected Type(Namespace @namespace, string name, string nativeName, string managedName) : base(name, nativeName, managedName)
        {
            Namespace = @namespace;
        }
    }
}
