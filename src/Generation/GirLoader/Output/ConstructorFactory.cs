using System;
using System.Collections.Generic;

namespace GirLoader.Output
{
    internal class ConstructorFactory
    {
        private readonly ReturnValueFactory _returnValueFactory;
        private readonly ParameterListFactory _parameterListFactory;

        public ConstructorFactory(ReturnValueFactory returnValueFactory, ParameterListFactory parameterListFactory)
        {
            _returnValueFactory = returnValueFactory;
            _parameterListFactory = parameterListFactory;
        }

        public Constructor Create(Input.Method method)
        {
            if (method.Name is null)
                throw new Exception("Methodinfo name is null");

            if (method.ReturnValue is null)
                throw new Exception($"{nameof(Input.Method)} {method.Name} {nameof(method.ReturnValue)} is null");

            if (method.Identifier is null)
                throw new Exception($"{nameof(Input.Method)} {method.Name} is missing {nameof(method.Identifier)} value");

            if (method.Name != string.Empty)
            {
                return new Constructor(
                    originalName: new SymbolName(method.Identifier),
                    name: method.Name,
                    returnValue: _returnValueFactory.Create(method.ReturnValue),
                    parameterList: _parameterListFactory.Create(method.Parameters, method.Throws)
                );
            }

            if (!string.IsNullOrEmpty(method.MovedTo))
                throw new ConstructorMovedException(method, $"Constructor {method.Identifier} moved to {method.MovedTo}.");

            throw new Exception($"{nameof(Input.Method)} {method.Identifier} has no {nameof(method.Name)} and did not move.");

        }

        public IEnumerable<Constructor> Create(IEnumerable<Input.Method> methods)
        {
            var list = new List<Constructor>();

            foreach (var method in methods)
            {
                try
                {
                    list.Add(Create(method));
                }
                catch (SingleParameterFactory.VarArgsNotSupportedException ex)
                {
                    Log.Verbose($"Constructor {method.Name} could not be created: {ex.Message}");
                }
                catch (ConstructorMovedException ex)
                {
                    Log.Verbose($"Constructor ignored: {ex.Message}");
                }
            }

            return list;
        }

        public class ConstructorMovedException : Exception
        {
            public Input.Method Method { get; }

            public ConstructorMovedException(Input.Method method, string message) : base(message)
            {
                Method = method;
            }
        }
    }
}
