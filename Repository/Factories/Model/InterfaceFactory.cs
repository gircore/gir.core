using System;
using Repository.Model;
using Repository.Services;
using Repository.Xml;

namespace Repository.Factories
{
    public interface IInterfaceFactory
    {
        Interface Create(InterfaceInfo iface, Namespace @namespace);
    }

    public class InterfaceFactory : IInterfaceFactory
    {
        private readonly ISymbolReferenceFactory _symbolReferenceFactory;

        public InterfaceFactory(ISymbolReferenceFactory symbolReferenceFactory)
        {
            _symbolReferenceFactory = symbolReferenceFactory;
        }

        public Interface Create(InterfaceInfo iface, Namespace @namespace)
        {
            if (iface.Name is null)
                throw new Exception("Interface is missing a name");

            if (iface.TypeName is null)
                throw new Exception($"Interface {iface.Name} is missing a {nameof(iface.TypeName)}");
            
            return new Interface(
                @namespace: @namespace, 
                nativeName: iface.Name, 
                managedName: iface.Name,
                cType: iface.TypeName,
                implements: _symbolReferenceFactory.Create(iface.Implements)
            );
        }
    }
}
