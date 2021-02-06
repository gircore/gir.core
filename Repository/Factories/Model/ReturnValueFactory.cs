using Repository.Model;
using Repository.Services;
using Repository.Xml;

namespace Repository.Factories
{
    public interface IReturnValueFactory
    {
        ReturnValue Create(ReturnValueInfo returnValueInfo);
    }

    public class ReturnValueFactory : IReturnValueFactory
    {
        private readonly ISymbolReferenceFactory _symbolReferenceFactory;

        public ReturnValueFactory(ISymbolReferenceFactory symbolReferenceFactory)
        {
            _symbolReferenceFactory = symbolReferenceFactory;
        }
        
        public ReturnValue Create(ReturnValueInfo returnValueInfo)
        {
            return new ReturnValue(
                symbolReference: _symbolReferenceFactory.Create(returnValueInfo)
            );
        }
    }
}
