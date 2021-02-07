using System;
using System.Linq;
using Repository.Model;
using Repository.Xml;

namespace Repository.Factories
{
    public interface IMethodFactory
    {
        Method Create(MethodInfo methodInfo, Namespace @namespace);
        Method CreateGetTypeMethod(string getTypeMethodName, Namespace @namespace);
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

        public Method Create(MethodInfo methodInfo, Namespace @namespace)
        {
            if (methodInfo.Name is null)
                throw new Exception("Methodinfo name is null");
            
            if (methodInfo.ReturnValue is null)
                throw new Exception($"{nameof(MethodInfo)} {methodInfo.Name} {nameof(methodInfo.ReturnValue)} is null");

            if(methodInfo.Identifier is null)
                throw new Exception($"{nameof(MethodInfo)} {methodInfo.Name} is missing {nameof(methodInfo.Identifier)} value");
            
            return new Method(
                @namespace: @namespace,
                nativeName: methodInfo.Identifier,
                managedName: methodInfo.Name,
                returnValue: _returnValueFactory.Create(methodInfo.ReturnValue),
                arguments: _argumentsFactory.Create(methodInfo.Parameters)
            );
        }

        public Method CreateGetTypeMethod(string getTypeMethodName, Namespace @namespace)
        {
            ReturnValue returnValue = _returnValueFactory.Create(
                type: "gulong", 
                isArray: false, 
                transfer: Transfer.None, 
                nullable: false
            );
            
            return new Method(
                @namespace: @namespace,
                nativeName: getTypeMethodName,
                managedName: "get_type",
                returnValue: returnValue,
                arguments: Enumerable.Empty<Argument>()
            );
        }
    }
}
