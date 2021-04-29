using System;
using Repository.Model;
using Repository.Services;
using Repository.Xml;

namespace Repository.Factories
{
    internal class InterfaceFactory
    {
        private readonly SymbolReferenceFactory _symbolReferenceFactory;
        private readonly MethodFactory _methodFactory;
        private readonly PropertyFactory _propertyFactory;

        public InterfaceFactory(SymbolReferenceFactory symbolReferenceFactory, MethodFactory methodFactory, PropertyFactory propertyFactory)
        {
            _symbolReferenceFactory = symbolReferenceFactory;
            _methodFactory = methodFactory;
            _propertyFactory = propertyFactory;
        }

        public Interface Create(InterfaceInfo iface, Namespace @namespace)
        {
            if (iface.Name is null)
                throw new Exception("Interface is missing a name");

            if (iface.TypeName is null)
                throw new Exception($"Interface {iface.Name} is missing a {nameof(iface.TypeName)}");
            
            if (iface.GetTypeFunction is null)
                throw new Exception($"Interface {iface.Name} is missing a {nameof(iface.GetTypeFunction)}");

            CTypeName? ctypeName = null;
            if (iface.Type is { })
                ctypeName = new CTypeName(iface.Type);
            
            return new Interface(
                @namespace: @namespace, 
                typeName: new TypeName(iface.Name),
                cTypeName:  ctypeName,
                symbolName: new SymbolName(iface.Name),
                implements: _symbolReferenceFactory.Create(iface.Implements, @namespace.Name),
                methods: _methodFactory.Create(iface.Methods, @namespace),
                functions: _methodFactory.Create(iface.Functions, @namespace),
                getTypeFunction: _methodFactory.CreateGetTypeMethod(iface.GetTypeFunction),
                properties: _propertyFactory.Create(iface.Properties, @namespace.Name)
            );
        }
    }
}
