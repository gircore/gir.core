using Repository.Model;
using Repository.Xml;

#nullable enable

namespace Repository.Factories
{
    public interface IEnumartionFactory
    {
        Enumeration Create(EnumInfo @enum, Namespace @namespace, bool hasFlags);
    }

    public class EnumartionFactory : IEnumartionFactory
    {
        public Enumeration Create(EnumInfo @enum, Namespace @namespace, bool hasFlags)
        {
            return new Enumeration()
            {
                Namespace = @namespace, 
                NativeName = @enum.Name, 
                ManagedName = @enum.Name, 
                HasFlags = hasFlags,
            };
        }
    }
}
