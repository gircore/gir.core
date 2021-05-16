using System;

namespace Gir.Output.Model
{
    internal class InterfaceFactory
    {
        private readonly TypeReferenceFactory _typeReferenceFactory;
        private readonly MethodFactory _methodFactory;
        private readonly PropertyFactory _propertyFactory;

        public InterfaceFactory(TypeReferenceFactory typeReferenceFactory, MethodFactory methodFactory, PropertyFactory propertyFactory)
        {
            _typeReferenceFactory = typeReferenceFactory;
            _methodFactory = methodFactory;
            _propertyFactory = propertyFactory;
        }

        public Interface Create(Input.Model.Interface @interface, Repository repository)
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
                repository: repository,
                typeName: new TypeName(@interface.Name),
                cTypeName: ctypeName,
                symbolName: new SymbolName(@interface.Name),
                implements: _typeReferenceFactory.Create(@interface.Implements, repository.Namespace.Name),
                methods: _methodFactory.Create(@interface.Methods, repository.Namespace.Name),
                functions: _methodFactory.Create(@interface.Functions, repository.Namespace.Name),
                getTypeFunction: _methodFactory.CreateGetTypeMethod(@interface.GetTypeFunction),
                properties: _propertyFactory.Create(@interface.Properties, repository.Namespace.Name)
            );
        }
    }
}
