using System;
using System.Linq;
using Repository.Model;
using Repository.Xml;

namespace Repository.Factories
{
    internal class CallbackFactory
    {
        private readonly ReturnValueFactory _returnValueFactory;
        private readonly ArgumentsFactory _argumentsFactory;
        private readonly CaseConverter _caseConverter;

        public CallbackFactory(ReturnValueFactory returnValueFactory, ArgumentsFactory argumentsFactory, CaseConverter caseConverter)
        {
            _returnValueFactory = returnValueFactory;
            _argumentsFactory = argumentsFactory;
            _caseConverter = caseConverter;
        }
        
        public Callback Create(CallbackInfo callbackInfo, Namespace @namespace)
        {
            if (callbackInfo.Name is null)
                throw new Exception("Callback is missing a name");

            if (callbackInfo.ReturnValue is null)
                throw new Exception($"Callback {callbackInfo.Name} is  missing a return value");

            return new Callback(
                @namespace: @namespace,
                name: callbackInfo.Name,
                managedName: _caseConverter.ToPascal(callbackInfo.Name),
                returnValue: _returnValueFactory.Create(callbackInfo.ReturnValue, @namespace.Name),
                arguments: _argumentsFactory.Create(callbackInfo.Parameters, @namespace.Name).ToList(),
                ctype: callbackInfo.Type
            );
        }
    }
}
