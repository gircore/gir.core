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
        private readonly IArgumentsFactory _argumentsFactory;

        public MethodFactory(IReturnValueFactory returnValueFactory, IArgumentsFactory argumentsFactory)
        {
            _returnValueFactory = returnValueFactory;
            _argumentsFactory = argumentsFactory;
        }

        public Method Create(MethodInfo methodInfo)
        {
            if (methodInfo.Name is null)
                throw new Exception("Methodinfo name is null");
            
            if (methodInfo.ReturnValue is null)
                throw new Exception($"{nameof(MethodInfo)} {methodInfo.Name} {nameof(methodInfo.ReturnValue)} is null");

            return new Method(
                name: methodInfo.Name,
                returnValue: _returnValueFactory.Create(methodInfo.ReturnValue),
                arguments: _argumentsFactory.Create(methodInfo.Parameters)
            );
        }
    }
}
