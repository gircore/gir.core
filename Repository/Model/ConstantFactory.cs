using System;

namespace Repository.Model
{
    internal class ConstantFactory
    {
        private readonly TypeReferenceFactory _typeReferenceFactory;
        private readonly IdentifierConverter _identifierConverter;

        public ConstantFactory(TypeReferenceFactory typeReferenceFactory, IdentifierConverter identifierConverter)
        {
            _typeReferenceFactory = typeReferenceFactory;
            _identifierConverter = identifierConverter;
        }

        public Constant Create(Xml.Constant constant, NamespaceName currentNamespace)
        {
            if (constant.Name is null)
                throw new Exception($"{nameof(Xml.Constant)} misses a {nameof(constant.Name)}");

            if (constant.Value is null)
                throw new Exception($"{nameof(Xml.Constant)} {constant.Name} misses a {nameof(constant.Value)}");

            return new Constant(
                elementName: new ElementName(_identifierConverter.EscapeIdentifier(constant.Name)),
                symbolName: new SymbolName(_identifierConverter.EscapeIdentifier(constant.Name)),
                typeReference: _typeReferenceFactory.Create(constant, currentNamespace),
                value: constant.Value
            );
        }
    }
}
