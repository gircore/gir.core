using System;
using System.Collections.Generic;

namespace Repository.Model
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

        public Method Create(Xml.Method method, Namespace @namespace)
        {
            if (method.Name is null)
                throw new Exception("Methodinfo name is null");

            if (method.ReturnValue is null)
                throw new Exception($"{nameof(Xml.Method)} {method.Name} {nameof(method.ReturnValue)} is null");

            if (method.Identifier is null)
                throw new Exception($"{nameof(Xml.Method)} {method.Name} is missing {nameof(method.Identifier)} value");

            if (method.Name != string.Empty)
            {
                return new Method(
                    elementName: new ElementName(method.Identifier),
                    symbolName: new SymbolName(_identifierConverter.EscapeIdentifier(_caseConverter.ToPascalCase(method.Name))),
                    returnValue: _returnValueFactory.Create(method.ReturnValue, @namespace.Name),
                    parameterList: _parameterListFactory.Create(method.Parameters, @namespace.Name, method.Throws)
                );
            }

            if (!string.IsNullOrEmpty(method.MovedTo))
                throw new MethodMovedException(method, $"Method {method.Identifier} moved to {method.MovedTo}.");

            throw new Exception($"{nameof(Xml.Method)} {method.Identifier} has no {nameof(method.Name)} and did not move.");

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

        public IEnumerable<Method> Create(IEnumerable<Xml.Method> methods, Namespace @namespace)
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
            public Xml.Method Method { get; }

            public MethodMovedException(Xml.Method method, string message) : base(message)
            {
                Method = method;
            }
        }
    }
}
