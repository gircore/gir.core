using System;
using System.Collections.Generic;
using System.Linq;
using Repository.Model;
using Repository.Xml;

namespace Repository.Factories
{
    public interface IMethodFactory
    {
        Method Create(MethodInfo methodInfo, Namespace @namespace);
        IEnumerable<Method> Create(IEnumerable<MethodInfo> methods, Namespace @namespace);
        Method CreateGetTypeMethod(string getTypeMethodName, Namespace @namespace);
    }

    public class MethodFactory : IMethodFactory
    {
        private readonly IReturnValueFactory _returnValueFactory;
        private readonly IArgumentsFactory _argumentsFactory;
        private readonly ICaseConverter _caseConverter;

        public MethodFactory(IReturnValueFactory returnValueFactory, IArgumentsFactory argumentsFactory, ICaseConverter caseConverter)
        {
            _returnValueFactory = returnValueFactory;
            _argumentsFactory = argumentsFactory;
            _caseConverter = caseConverter;
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
                managedName: _caseConverter.ToPascalCase(methodInfo.Name),
                returnValue: _returnValueFactory.Create(methodInfo.ReturnValue),
                arguments: _argumentsFactory.Create(methodInfo.Parameters, methodInfo.Throws)
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
                managedName: "GetType",
                returnValue: returnValue,
                arguments: Enumerable.Empty<Argument>()
            );
        }
        
        public IEnumerable<Method> Create(IEnumerable<MethodInfo> methods, Namespace @namespace)
        {
            var list = new List<Method>();

            foreach (var method in methods)
            {
                try
                {
                    list.Add(Create(method, @namespace));
                }
                catch (ArgumentFactory.VarArgsNotSupportedException ex)
                {
                    Log.Debug($"Method {method.Name} could not be created: {ex.Message}");
                }
            }

            return list;
        }
    }
}
