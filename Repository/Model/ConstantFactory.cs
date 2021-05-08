using System;

namespace Repository.Model
{
    internal class ConstantFactory
    {
        private readonly SymbolReferenceFactory _symbolReferenceFactory;
        private readonly IdentifierConverter _identifierConverter;

        public ConstantFactory(SymbolReferenceFactory symbolReferenceFactory, IdentifierConverter identifierConverter)
        {
            _symbolReferenceFactory = symbolReferenceFactory;
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
                symbolReference: _symbolReferenceFactory.Create(constant, currentNamespace),
                value: constant.Value
            );
        }
    }
}
