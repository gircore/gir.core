using System;
using Repository.Model;
using Repository.Xml;

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
            if (iface.Name is null)
                throw new Exception("Interface is missing a name");
            
            return new Interface(
                @namespace: @namespace, 
                nativeName: iface.Name, 
                managedName: iface.Name
            );
        }
    }
}
