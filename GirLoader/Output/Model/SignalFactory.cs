using System;
using System.Collections.Generic;
using System.Linq;

namespace Gir.Output.Model
{
    internal class SignalFactory
    {
        private readonly ReturnValueFactory _returnValueFactory;
        private readonly ParameterListFactory _parameterListFactory;

        public SignalFactory(ReturnValueFactory returnValueFactory, ParameterListFactory parameterListFactory)
        {
            _returnValueFactory = returnValueFactory;
            _parameterListFactory = parameterListFactory;
        }

        public Signal Create(Input.Model.Signal signal, NamespaceName namespaceName)
        {
            if (signal.Name is null)
                throw new Exception($"{nameof(signal)} is missing a {nameof(signal.Name)}");

            if (signal.ReturnValue is null)
                throw new Exception($"{nameof(Input.Model.Signal)} {signal.Name} {nameof(signal.ReturnValue)} is null");

            return new Signal(
                elementName: new ElementName(signal.Name),
                symbolName: new SymbolName(Helper.String.ToPascalCase(signal.Name)),
                returnValue: _returnValueFactory.Create(signal.ReturnValue, namespaceName),
                parameterList: _parameterListFactory.Create(signal.Parameters, namespaceName)
            );
        }

        public IEnumerable<Signal> Create(IEnumerable<Input.Model.Signal> signals, NamespaceName namespaceName)
            => signals.Select(x => Create(x, namespaceName)).ToList();
    }
}
