using Repository.Model;
using Repository.Xml;

#nullable enable

namespace Repository.Factories
{
    public interface IInterfaceFactory
    {
        Interface Create(InterfaceInfo iface, Namespace @namespace);
    }

    public class InterfaceFactory : IInterfaceFactory
    {
        public Interface Create(InterfaceInfo iface, Namespace @namespace)
        {
            return new Interface(
                @namespace: @namespace, 
                nativeName: iface.Name, 
                managedName: iface.Name
            );
        }
    }
}
