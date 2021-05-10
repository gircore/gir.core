namespace Repository.Model
{
    internal class ReturnValueFactory
    {
        private readonly TypeReferenceFactory _typeReferenceFactory;
        private readonly TransferFactory _transferFactory;
        private readonly TypeInformationFactory _typeInformationFactory;

        public ReturnValueFactory(TypeReferenceFactory typeReferenceFactory, TransferFactory transferFactory, TypeInformationFactory typeInformationFactory)
        {
            _typeReferenceFactory = typeReferenceFactory;
            _transferFactory = transferFactory;
            _typeInformationFactory = typeInformationFactory;
        }

        public ReturnValue Create(Xml.ReturnValue returnValue, NamespaceName namespaceName)
        {
            return new ReturnValue(
                typeReference: _typeReferenceFactory.Create(returnValue, namespaceName),
                transfer: _transferFactory.FromText(returnValue.TransferOwnership),
                nullable: returnValue.Nullable,
                typeInformation: _typeInformationFactory.Create(returnValue)
            );
        }

        public ReturnValue Create(string type, Transfer transfer, bool nullable, NamespaceName namespaceName)
        {
            return new ReturnValue(
                typeReference: _typeReferenceFactory.Create(type, type, namespaceName),
                transfer: transfer,
                nullable: nullable,
                typeInformation: _typeInformationFactory.CreateDefault()
            );
        }
    }
}
