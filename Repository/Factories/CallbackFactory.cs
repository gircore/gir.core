using System.Linq;
using Repository.Model;
using Repository.Xml;

#nullable enable

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
            return new Callback()
            {
                Namespace = @namespace,
                NativeName = callbackInfo.Name,
                ManagedName = callbackInfo.Name,
                ReturnValue = _returnValueFactory.Create(callbackInfo.ReturnValue),
                Arguments = _argumentsFactory.Create(callbackInfo.Parameters).ToList()
            };
        }
    }
}
