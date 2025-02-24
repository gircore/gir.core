using System;

namespace GirLoader.Output;

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

        return new Callback(
            repository: repository,
            ctype: callback.Type,
            name: callback.Name,
            returnValue: _returnValueFactory.Create(callback.ReturnValue),
            parameterList: _parameterListFactory.Create(callback.Parameters),
            introspectable: callback.Introspectable,
            throws: callback.Throws
        );
    }
}
