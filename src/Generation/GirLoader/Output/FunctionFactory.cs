using System;
using System.Collections.Generic;

namespace GirLoader.Output;

internal class FunctionFactory
{
    private readonly ReturnValueFactory _returnValueFactory;
    private readonly ParameterListFactory _parameterListFactory;

    public FunctionFactory(ReturnValueFactory returnValueFactory, ParameterListFactory parameterListFactory)
    {
        _returnValueFactory = returnValueFactory;
        _parameterListFactory = parameterListFactory;
    }

    public Function Create(Input.Method method, Repository repository)
    {
        if (method.Name is null)
            throw new Exception("Methodinfo name is null");

        if (method.ReturnValue is null)
            throw new Exception($"{nameof(Input.Method)} {method.Name} {nameof(method.ReturnValue)} is null");

        if (method.Identifier is null)
            throw new Exception($"{nameof(Input.Method)} {method.Name} is missing {nameof(method.Identifier)} value");

        if (method.Name != string.Empty)
        {
            return new Function(
                repository: repository,
                identifier: method.Identifier,
                name: method.Name,
                returnValue: _returnValueFactory.Create(method.ReturnValue),
                parameterList: _parameterListFactory.Create(method.Parameters),
                throws: method.Throws,
                introspectable: method.Introspectable,
                version: method.Version
            );
        }

        if (!string.IsNullOrEmpty(method.MovedTo))
            throw new FunctionMovedException(method, $"Method {method.Identifier} moved to {method.MovedTo}.");

        throw new Exception($"{nameof(Input.Method)} {method.Identifier} has no {nameof(method.Name)} and did not move.");

    }

    public Function CreateGetTypeFunction(string getTypeMethodName, Repository repository)
    {
        ReturnValue returnValue = _returnValueFactory.Create(
            ctype: "gsize",
            transfer: Transfer.None,
            nullable: false
        );

        return new Function(
            repository: repository,
            identifier: getTypeMethodName,
            name: "GetGType",
            returnValue: returnValue,
            parameterList: new ParameterList(),
            throws: false,
            introspectable: true,
            version: null
        );
    }

    public IEnumerable<Function> Create(IEnumerable<Input.Method> methods, Repository repository)
    {
        var list = new List<Function>();

        foreach (var method in methods)
        {
            try
            {
                list.Add(Create(method, repository));
            }
            catch (FunctionMovedException ex)
            {
                Log.Verbose($"Function ignored: {ex.Message}");
            }
        }

        return list;
    }

    public class FunctionMovedException : Exception
    {
        public Input.Method Method { get; }

        public FunctionMovedException(Input.Method method, string message) : base(message)
        {
            Method = method;
        }
    }
}
