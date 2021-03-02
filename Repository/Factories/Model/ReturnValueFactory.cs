using Repository.Model;
using Repository.Services;
using Repository.Xml;

namespace Repository.Factories
{
    internal class ReturnValueFactory
    {
        private readonly SymbolReferenceFactory _symbolReferenceFactory;
        private readonly TransferFactory _transferFactory;

        public ReturnValueFactory(SymbolReferenceFactory symbolReferenceFactory, TransferFactory transferFactory)
        {
            _symbolReferenceFactory = symbolReferenceFactory;
            _transferFactory = transferFactory;
        }
        
        public ReturnValue Create(ReturnValueInfo returnValueInfo)
        {
            return new ReturnValue(
                symbolReference: _symbolReferenceFactory.Create(returnValueInfo),
                transfer: _transferFactory.FromText(returnValueInfo.TransferOwnership),
                nullable: returnValueInfo.Nullable
            );
        }

        public ReturnValue Create(string type, Array? array, Transfer transfer, bool nullable)
        {
            return new ReturnValue(
                symbolReference: _symbolReferenceFactory.Create(type, array),
                transfer: transfer,
                nullable: nullable
            );
        }
    }
}
