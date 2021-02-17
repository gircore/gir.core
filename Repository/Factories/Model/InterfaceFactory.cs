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
        private readonly SymbolReferenceFactory _symbolReferenceFactory;
        private readonly IMethodFactory _methodFactory;

        public InterfaceFactory(SymbolReferenceFactory symbolReferenceFactory, IMethodFactory methodFactory)
        {
            _symbolReferenceFactory = symbolReferenceFactory;
            _methodFactory = methodFactory;
        }

        public Interface Create(InterfaceInfo iface, Namespace @namespace)
        {
            if (iface.Name is null)
                throw new Exception("Interface is missing a name");

            if (iface.TypeName is null)
                throw new Exception($"Interface {iface.Name} is missing a {nameof(iface.TypeName)}");
            
            if (iface.GetTypeFunction is null)
                throw new Exception($"Interface {iface.Name} is missing a {nameof(iface.GetTypeFunction)}");
            
            return new Interface(
                @namespace: @namespace, 
                nativeName: iface.Name, 
                managedName: iface.Name,
                cType: iface.TypeName,
                implements: _symbolReferenceFactory.Create(iface.Implements),
                methods: _methodFactory.Create(iface.Methods, @namespace),
                functions: _methodFactory.Create(iface.Functions, @namespace),
                getTypeFunction: _methodFactory.CreateGetTypeMethod(iface.GetTypeFunction, @namespace)
            );
        }
    }
}
