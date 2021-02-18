using System;
using System.Linq;
using Repository.Model;
using Repository.Xml;

namespace Repository.Factories
{
    public class CallbackFactory
    {
        private readonly ReturnValueFactory _returnValueFactory;
        private readonly ArgumentsFactory _argumentsFactory;

        public CallbackFactory(ReturnValueFactory returnValueFactory, ArgumentsFactory argumentsFactory)
        {
            _returnValueFactory = returnValueFactory;
            _argumentsFactory = argumentsFactory;
        }
        
        public Callback Create(CallbackInfo callbackInfo, Namespace @namespace)
        {
            if (callbackInfo.Name is null)
                throw new Exception("Callback is missing a name");

            if (callbackInfo.ReturnValue is null)
                throw new Exception($"Callback {callbackInfo.Name} is  missing a return value");
            
            return new Callback(
                @namespace: @namespace,
                nativeName: callbackInfo.Name,
                managedName: callbackInfo.Name,
                returnValue: _returnValueFactory.Create(callbackInfo.ReturnValue),
                arguments: _argumentsFactory.Create(callbackInfo.Parameters).ToList()
            );
        }
    }
}
