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
        private readonly ParameterListFactory _parameterListFactory;
        private readonly CaseConverter _caseConverter;
        private readonly IdentifierConverter _identifierConverter;

        public MethodFactory(ReturnValueFactory returnValueFactory, ParameterListFactory parameterListFactory, CaseConverter caseConverter, IdentifierConverter identifierConverter)
        {
            _returnValueFactory = returnValueFactory;
            _parameterListFactory = parameterListFactory;
            _caseConverter = caseConverter;
            _identifierConverter = identifierConverter;
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
                    elementName: new ElementName(methodInfo.Identifier),
                    symbolName: new SymbolName(_identifierConverter.EscapeIdentifier(_caseConverter.ToPascalCase(methodInfo.Name))),
                    returnValue: _returnValueFactory.Create(methodInfo.ReturnValue, @namespace.Name),
                    parameterList: _parameterListFactory.Create(methodInfo.Parameters, @namespace.Name, methodInfo.Throws)
                );
            }

            if (!string.IsNullOrEmpty(methodInfo.MovedTo))
                throw new MethodMovedException(methodInfo, $"Method {methodInfo.Identifier} moved to {methodInfo.MovedTo}.");

            throw new Exception($"{nameof(MethodInfo)} {methodInfo.Identifier} has no {nameof(methodInfo.Name)} and did not move.");

        }

        public Method CreateGetTypeMethod(string getTypeMethodName)
        {
            ReturnValue returnValue = _returnValueFactory.Create(
                type: "GType",
                transfer: Transfer.None,
                nullable: false,
                namespaceName: new NamespaceName("GLib")
            );

            return new Method(
                elementName: new ElementName(getTypeMethodName),
                symbolName: new SymbolName("GetGType"),
                returnValue: returnValue,
                parameterList: new ParameterList()
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
                catch (SingleParameterFactory.VarArgsNotSupportedException ex)
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
