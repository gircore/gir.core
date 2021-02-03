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
            if (methodInfo.ReturnValue is null)
                throw new Exception("Methods ReturnValue is null");

            return new Method(
                ReturnValue: _returnValueFactory.Create(methodInfo.ReturnValue)
            );
        }
    }
}
