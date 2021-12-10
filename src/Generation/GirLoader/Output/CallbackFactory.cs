using System;

namespace GirLoader.Output
{
    internal class CallbackFactory
    {
        private readonly ReturnValueFactory _returnValueFactory;
        private readonly ParameterListFactory _parameterListFactory;

        public CallbackFactory(ReturnValueFactory returnValueFactory, ParameterListFactory parameterListFactory)
        {
            _returnValueFactory = returnValueFactory;
            _parameterListFactory = parameterListFactory;
        }

        public Callback Create(Input.Callback callback, Repository repository)
        {
            if (callback.Name is null)
                throw new Exception("Callback is missing a name");

            if (callback.ReturnValue is null)
                throw new Exception($"Callback {callback.Name} is  missing a return value");

            CType? cTypeName = null;
            if (callback.Type is { })
                cTypeName = new CType(callback.Type);

            return new Callback(
                repository: repository,
                ctype: cTypeName,
                originalName: new TypeName(callback.Name),
                returnValue: _returnValueFactory.Create(callback.ReturnValue),
                parameterList: _parameterListFactory.Create(callback.Parameters)
            );
        }
    }
}
