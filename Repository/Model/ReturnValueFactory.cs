namespace Repository.Model
{
    internal class ReturnValueFactory
    {
        private readonly SymbolReferenceFactory _symbolReferenceFactory;
        private readonly TransferFactory _transferFactory;
        private readonly TypeInformationFactory _typeInformationFactory;

        public ReturnValueFactory(SymbolReferenceFactory symbolReferenceFactory, TransferFactory transferFactory, TypeInformationFactory typeInformationFactory)
        {
            _symbolReferenceFactory = symbolReferenceFactory;
            _transferFactory = transferFactory;
            _typeInformationFactory = typeInformationFactory;
        }

        public ReturnValue Create(Xml.ReturnValue returnValue, NamespaceName namespaceName)
        {
            return new ReturnValue(
                symbolReference: _symbolReferenceFactory.Create(returnValue, namespaceName),
                transfer: _transferFactory.FromText(returnValue.TransferOwnership),
                nullable: returnValue.Nullable,
                typeInformation: _typeInformationFactory.Create(returnValue)
            );
        }

        public ReturnValue Create(string type, Transfer transfer, bool nullable, NamespaceName namespaceName)
        {
            return new ReturnValue(
                symbolReference: _symbolReferenceFactory.Create(type, type, namespaceName),
                transfer: transfer,
                nullable: nullable,
                typeInformation: _typeInformationFactory.CreateDefault()
            );
        }
    }
}
