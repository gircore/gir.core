using System;
using System.Collections.Generic;
using System.Linq;

namespace Repository.Model
{
    internal class SignalFactory
    {
        private readonly ReturnValueFactory _returnValueFactory;
        private readonly CaseConverter _caseConverter;
        private readonly ParameterListFactory _parameterListFactory;

        public SignalFactory(ReturnValueFactory returnValueFactory, CaseConverter caseConverter, ParameterListFactory parameterListFactory)
        {
            _returnValueFactory = returnValueFactory;
            _caseConverter = caseConverter;
            _parameterListFactory = parameterListFactory;
        }

        public Signal Create(Xml.Signal signal, NamespaceName namespaceName)
        {
            if (signal.Name is null)
                throw new Exception($"{nameof(signal)} is missing a {nameof(signal.Name)}");

            if (signal.ReturnValue is null)
                throw new Exception($"{nameof(Xml.Signal)} {signal.Name} {nameof(signal.ReturnValue)} is null");

            return new Signal(
                elementName: new ElementName(signal.Name),
                symbolName: new SymbolName(_caseConverter.ToPascalCase(signal.Name)),
                returnValue: _returnValueFactory.Create(signal.ReturnValue, namespaceName),
                parameterList: _parameterListFactory.Create(signal.Parameters, namespaceName)
            );
        }

        public IEnumerable<Signal> Create(IEnumerable<Xml.Signal> signals, NamespaceName namespaceName)
            => signals.Select(x => Create(x, namespaceName)).ToList();
    }
}
