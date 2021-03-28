using System;
using System.Collections.Generic;
using System.Linq;
using Repository.Analysis;
using Repository.Model;
using Repository.Xml;

namespace Repository.Factories
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

        public Signal Create(SignalInfo signalInfo, NamespaceName namespaceName)
        {
            if (signalInfo.Name is null)
                throw new Exception($"{nameof(signalInfo)} is missing a {nameof(signalInfo.Name)}");

            if (signalInfo.ReturnValue is null)
                throw new Exception($"{nameof(SignalInfo)} {signalInfo.Name} {nameof(signalInfo.ReturnValue)} is null");

            return new Signal(
                elementName: new ElementName(signalInfo.Name),
                symbolName: new SymbolName(_caseConverter.ToPascalCase(signalInfo.Name)),
                returnValue: _returnValueFactory.Create(signalInfo.ReturnValue, namespaceName),
                parameterList: _parameterListFactory.Create(signalInfo.Parameters, namespaceName)
            );
        }

        public IEnumerable<Signal> Create(IEnumerable<SignalInfo> signals, NamespaceName namespaceName)
            => signals.Select(x => Create(x, namespaceName)).ToList();
    }
}
