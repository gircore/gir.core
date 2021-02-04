using System;
using Repository.Model;
using Repository.Xml;

namespace Repository.Factories
{
    public interface IMethodFactory
    {
        Method Create(MethodInfo methodInfo);
    }

    public class MethodFactory : IMethodFactory
    {
        private readonly IReturnValueFactory _returnValueFactory;

        public MethodFactory(IReturnValueFactory returnValueFactory)
        {
            _returnValueFactory = returnValueFactory;
        }

        public Method Create(MethodInfo methodInfo)
        {
            if (methodInfo.Name is null)
                throw new Exception("Methodinfo name is null");
            
            if (methodInfo.ReturnValue is null)
                throw new Exception($"{nameof(MethodInfo)} {methodInfo.Name} {nameof(methodInfo.ReturnValue)} is null");

            return new Method(
                name: methodInfo.Name,
                returnValue: _returnValueFactory.Create(methodInfo.ReturnValue)
            );
        }
    }
}
