using Repository.Analysis;
using Repository.Factories.Model;
using Repository.Model;
using Repository.Services;
using Repository.Xml;

namespace Repository.Factories
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

        public ReturnValue Create(ReturnValueInfo returnValueInfo, NamespaceName namespaceName)
        {
            return new ReturnValue(
                symbolReference: _symbolReferenceFactory.Create(returnValueInfo, namespaceName),
                transfer: _transferFactory.FromText(returnValueInfo.TransferOwnership),
                nullable: returnValueInfo.Nullable,
                typeInformation: _typeInformationFactory.Create(returnValueInfo)
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
