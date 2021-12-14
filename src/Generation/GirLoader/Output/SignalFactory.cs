using System;
using System.Collections.Generic;
using System.Linq;

namespace GirLoader.Output
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

        public Signal Create(Input.Signal signal)
        {
            if (signal.Name is null)
                throw new Exception($"{nameof(signal)} is missing a {nameof(signal.Name)}");

            if (signal.ReturnValue is null)
                throw new Exception($"{nameof(Input.Signal)} {signal.Name} {nameof(signal.ReturnValue)} is null");

            return new Signal(
                name: signal.Name,
                returnValue: _returnValueFactory.Create(signal.ReturnValue),
                parameterList: _parameterListFactory.Create(signal.Parameters)
            );
        }

        public IEnumerable<Signal> Create(IEnumerable<Input.Signal> signals)
            => signals.Select(Create).ToList();
    }
}
