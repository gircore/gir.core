using Repository.Model;
using Repository.Services;
using Repository.Xml;

#nullable enable

namespace Repository.Factories
{
    public interface IReturnValueFactory
    {
        ReturnValue Create(ReturnValueInfo returnValueInfo);
    }

    public class ReturnValueFactory : IReturnValueFactory
    {
        private readonly ITypeReferenceFactory _typeReferenceFactory;

        public ReturnValueFactory(ITypeReferenceFactory typeReferenceFactory)
        {
            _typeReferenceFactory = typeReferenceFactory;
        }
        
        public ReturnValue Create(ReturnValueInfo returnValueInfo)
        {
            return new ReturnValue(
                Type: _typeReferenceFactory.Create(returnValueInfo)
            );
        }
    }
}
