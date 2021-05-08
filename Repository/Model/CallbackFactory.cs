using System;

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

        public Callback Create(Xml.Callback callback, Namespace @namespace)
        {
            if (callback.Name is null)
                throw new Exception("Callback is missing a name");

            if (callback.ReturnValue is null)
                throw new Exception($"Callback {callback.Name} is  missing a return value");

            CTypeName? cTypeName = null;
            if (callback.Type is { })
                cTypeName = new CTypeName(callback.Type);

            return new Callback(
                @namespace: @namespace,
                ctypeName: cTypeName,
                typeName: new TypeName(callback.Name),
                symbolName: new SymbolName(_caseConverter.ToPascal(callback.Name)),
                returnValue: _returnValueFactory.Create(callback.ReturnValue, @namespace.Name),
                parameterList: _parameterListFactory.Create(callback.Parameters, @namespace.Name)
            );
        }
    }
}
