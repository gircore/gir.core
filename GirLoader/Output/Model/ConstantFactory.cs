using System;

namespace GirLoader.Output.Model
{
    internal class ConstantFactory
    {
        private readonly TypeReferenceFactory _typeReferenceFactory;

        public ConstantFactory(TypeReferenceFactory typeReferenceFactory)
        {
            _typeReferenceFactory = typeReferenceFactory;
        }

        public Constant Create(Input.Model.Constant constant, NamespaceName currentNamespace)
        {
            if (constant.Name is null)
                throw new Exception($"{nameof(Input.Model.Constant)} misses a {nameof(constant.Name)}");

            if (constant.Value is null)
                throw new Exception($"{nameof(Input.Model.Constant)} {constant.Name} misses a {nameof(constant.Value)}");

            return new Constant(
                elementName: new ElementName(Helper.String.EscapeIdentifier(constant.Name)),
                symbolName: new SymbolName(Helper.String.EscapeIdentifier(constant.Name)),
                typeReference: _typeReferenceFactory.Create(constant, currentNamespace),
                value: constant.Value
            );
        }
    }
}
