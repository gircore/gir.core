using System;
using Repository.Xml;

namespace Repository.Model
{
    internal class CallbackFactory
    {
        private readonly ReturnValueFactory _returnValueFactory;
        private readonly ParameterListFactory _parameterListFactory;
        private readonly CaseConverter _caseConverter;

        public CallbackFactory(ReturnValueFactory returnValueFactory, ParameterListFactory parameterListFactory, CaseConverter caseConverter)
        {
            _returnValueFactory = returnValueFactory;
            _parameterListFactory = parameterListFactory;
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
                symbolName: new SymbolName(_caseConverter.ToPascal(callbackInfo.Name)),
                returnValue: _returnValueFactory.Create(callbackInfo.ReturnValue, @namespace.Name),
                parameterList: _parameterListFactory.Create(callbackInfo.Parameters, @namespace.Name)
            );
        }
    }
}
