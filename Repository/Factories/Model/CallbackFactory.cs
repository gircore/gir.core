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

            CTypeName? cTypeName = null;
            if (callbackInfo.Type is { })
                cTypeName = new CTypeName(callbackInfo.Type);
            
            return new Callback(
                @namespace: @namespace,
                ctypeName: cTypeName,
                typeName: new TypeName(callbackInfo.Name),
                managedName: new ManagedName(_caseConverter.ToPascal(callbackInfo.Name)),
                nativeName: new NativeName(_caseConverter.ToPascal(callbackInfo.Name)),
                returnValue: _returnValueFactory.Create(callbackInfo.ReturnValue, @namespace.Name),
                arguments: _argumentsFactory.Create(callbackInfo.Parameters, @namespace.Name).ToList()
            );
        }
    }
}
