using System;
using System.Collections.Generic;
using System.Linq;
using Repository.Model;
using Repository.Xml;

namespace Repository.Factories
{
    internal class MethodFactory
    {
        private readonly ReturnValueFactory _returnValueFactory;
        private readonly ArgumentsFactory _argumentsFactory;
        private readonly CaseConverter _caseConverter;

        public MethodFactory(ReturnValueFactory returnValueFactory, ArgumentsFactory argumentsFactory, CaseConverter caseConverter)
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

            if (methodInfo.Identifier is null)
                throw new Exception($"{nameof(MethodInfo)} {methodInfo.Name} is missing {nameof(methodInfo.Identifier)} value");

            if (methodInfo.Name != string.Empty)
            {
                return new Method(
                    @namespace: @namespace,
                    name: methodInfo.Identifier,
                    managedName: _caseConverter.ToPascalCase(methodInfo.Name),
                    returnValue: _returnValueFactory.Create(methodInfo.ReturnValue),
                    arguments: _argumentsFactory.Create(methodInfo.Parameters, methodInfo.Throws)
                );
            }

            if (!string.IsNullOrEmpty(methodInfo.MovedTo))
                throw new MethodMovedException(methodInfo, $"Method {methodInfo.Identifier} moved to {methodInfo.MovedTo}.");

            throw new Exception($"{nameof(MethodInfo)} {methodInfo.Identifier} has no {nameof(methodInfo.Name)} and did not move.");

        }

        public Method CreateGetTypeMethod(string getTypeMethodName, Namespace @namespace)
        {
            ReturnValue returnValue = _returnValueFactory.Create(
                type: "gulong",
                array: null,
                transfer: Transfer.None,
                nullable: false
            );

            return new Method(
                @namespace: @namespace,
                name: getTypeMethodName,
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
                catch (MethodMovedException ex)
                {
                    Log.Debug($"Method ignored: {ex.Message}");
                }
            }

            return list;
        }

        public class MethodMovedException : Exception
        {
            public MethodInfo MethodInfo { get; }

            public MethodMovedException(MethodInfo methodInfo, string message) : base(message)
            {
                MethodInfo = methodInfo;
            }
        }
    }
}
