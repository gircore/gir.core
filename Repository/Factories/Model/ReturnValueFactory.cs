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
        private readonly ArrayFactory _arrayFactory;

        public ReturnValueFactory(SymbolReferenceFactory symbolReferenceFactory, TransferFactory transferFactory, ArrayFactory arrayFactory)
        {
            _symbolReferenceFactory = symbolReferenceFactory;
            _transferFactory = transferFactory;
            _arrayFactory = arrayFactory;
        }
        
        public ReturnValue Create(ReturnValueInfo returnValueInfo)
        {
            return new ReturnValue(
                symbolReference: _symbolReferenceFactory.Create(returnValueInfo),
                transfer: _transferFactory.FromText(returnValueInfo.TransferOwnership),
                nullable: returnValueInfo.Nullable,
                array: _arrayFactory.Create(returnValueInfo.Array)
            );
        }

        public ReturnValue Create(string type, Transfer transfer, bool nullable, Array? array = null)
        {
            return new ReturnValue(
                symbolReference: _symbolReferenceFactory.Create(type),
                transfer: transfer,
                nullable: nullable,
                array: array
            );
        }
    }
}
