using System;

namespace Gir.Model
{
    internal class ConstantFactory
    {
        private readonly TypeReferenceFactory _typeReferenceFactory;

        public ConstantFactory(TypeReferenceFactory typeReferenceFactory)
        {
            _typeReferenceFactory = typeReferenceFactory;
        }

        public Constant Create(Xml.Constant constant, NamespaceName currentNamespace)
        {
            if (constant.Name is null)
                throw new Exception($"{nameof(Xml.Constant)} misses a {nameof(constant.Name)}");

            if (constant.Value is null)
                throw new Exception($"{nameof(Xml.Constant)} {constant.Name} misses a {nameof(constant.Value)}");

            return new Constant(
                elementName: new ElementName(IdentifierConverter.EscapeIdentifier(constant.Name)),
                symbolName: new SymbolName(IdentifierConverter.EscapeIdentifier(constant.Name)),
                typeReference: _typeReferenceFactory.Create(constant, currentNamespace),
                value: constant.Value
            );
        }
    }
}
