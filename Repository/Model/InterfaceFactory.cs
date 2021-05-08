using System;

namespace Repository.Model
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

        public Interface Create(Xml.Interface @interface, Namespace @namespace)
        {
            if (@interface.Name is null)
                throw new Exception("Interface is missing a name");

            if (@interface.TypeName is null)
                throw new Exception($"Interface {@interface.Name} is missing a {nameof(@interface.TypeName)}");

            if (@interface.GetTypeFunction is null)
                throw new Exception($"Interface {@interface.Name} is missing a {nameof(@interface.GetTypeFunction)}");

            CTypeName? ctypeName = null;
            if (@interface.Type is { })
                ctypeName = new CTypeName(@interface.Type);

            return new Interface(
                @namespace: @namespace,
                typeName: new TypeName(@interface.Name),
                cTypeName: ctypeName,
                symbolName: new SymbolName(@interface.Name),
                implements: _symbolReferenceFactory.Create(@interface.Implements, @namespace.Name),
                methods: _methodFactory.Create(@interface.Methods, @namespace),
                functions: _methodFactory.Create(@interface.Functions, @namespace),
                getTypeFunction: _methodFactory.CreateGetTypeMethod(@interface.GetTypeFunction),
                properties: _propertyFactory.Create(@interface.Properties, @namespace.Name)
            );
        }
    }
}
