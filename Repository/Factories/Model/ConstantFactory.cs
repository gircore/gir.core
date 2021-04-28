using System;
using Repository.Analysis;
using Repository.Model;
using Repository.Services;
using Repository.Xml;

namespace Repository.Factories
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

        public Constant Create(ConstantInfo constantInfo, NamespaceName currentNamespace)
        {
            if (constantInfo.Name is null)
                throw new Exception($"{nameof(ConstantInfo)} misses a {nameof(constantInfo.Name)}");

            if (constantInfo.Value is null)
                throw new Exception($"{nameof(ConstantInfo)} {constantInfo.Name} misses a {nameof(constantInfo.Value)}");
            
            return new Constant(
                elementName: new ElementName(_identifierConverter.EscapeIdentifier(constantInfo.Name)),
                symbolName: new SymbolName(_identifierConverter.EscapeIdentifier(constantInfo.Name)),
                symbolReference: _symbolReferenceFactory.Create(constantInfo, currentNamespace),
                value: constantInfo.Value
            );
        }
    }
}
