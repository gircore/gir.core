using System;
using System.Linq;
using Repository.Model;
using Repository.Xml;

namespace Repository.Factories
{
    public interface ICallbackFactory
    {
        Callback Create(CallbackInfo callbackInfo, Namespace @namespace);
    }

    public class CallbackFactory : ICallbackFactory
    {
        private readonly IReturnValueFactory _returnValueFactory;
        private readonly IArgumentsFactory _argumentsFactory;

        public CallbackFactory(IReturnValueFactory returnValueFactory, IArgumentsFactory argumentsFactory)
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
