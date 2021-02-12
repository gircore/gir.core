using System;
using System.Collections.Generic;
using System.Linq;
using Repository.Model;
using Repository.Xml;

namespace Repository.Factories
{
    public interface ISignalFactory
    {
        Signal Create(SignalInfo signalInfo);
        IEnumerable<Signal> Create(IEnumerable<SignalInfo> signals);
    }

    public class SignalFactory : ISignalFactory
    {
        private readonly IReturnValueFactory _returnValueFactory;
        private readonly ICaseConverter _caseConverter;
        private readonly IArgumentsFactory _argumentsFactory;

        public SignalFactory(IReturnValueFactory returnValueFactory, ICaseConverter caseConverter, IArgumentsFactory argumentsFactory)
        {
            _returnValueFactory = returnValueFactory;
            _caseConverter = caseConverter;
            _argumentsFactory = argumentsFactory;
        }

        public Signal Create(SignalInfo signalInfo)
        {
            if (signalInfo.Name is null)
                throw new Exception($"{nameof(signalInfo)} is missing a {nameof(signalInfo.Name)}");

            if (signalInfo.ReturnValue is null)
                throw new Exception($"{nameof(SignalInfo)} {signalInfo.Name} {nameof(signalInfo.ReturnValue)} is null");

            return new Signal(
                nativeName: signalInfo.Name,
                managedName: _caseConverter.ToPascalCase(signalInfo.Name),
                returnValue: _returnValueFactory.Create(signalInfo.ReturnValue),
                arguments: _argumentsFactory.Create(signalInfo.Parameters)
            );
        }

        public IEnumerable<Signal> Create(IEnumerable<SignalInfo> signals)
            => signals.Select(Create).ToList();
    }
}
