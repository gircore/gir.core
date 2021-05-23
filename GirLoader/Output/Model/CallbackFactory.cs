using System;

namespace GirLoader.Output.Model
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

        public Callback Create(Input.Model.Callback callback, Repository repository)
        {
            if (callback.Name is null)
                throw new Exception("Callback is missing a name");

            if (callback.ReturnValue is null)
                throw new Exception($"Callback {callback.Name} is  missing a return value");

            CTypeName? cTypeName = null;
            if (callback.Type is { })
                cTypeName = new CTypeName(callback.Type);

            return new Callback(
                repository: repository,
                ctypeName: cTypeName,
                originalName: new SymbolName(callback.Name),
                symbolName: new SymbolName(new Helper.String(callback.Name).ToPascalCase()),
                returnValue: _returnValueFactory.Create(callback.ReturnValue, repository.Namespace.Name),
                parameterList: _parameterListFactory.Create(callback.Parameters, repository.Namespace.Name)
            );
        }
    }
}
