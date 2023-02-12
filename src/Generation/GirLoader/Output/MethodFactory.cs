using System;
using System.Collections.Generic;

namespace GirLoader.Output;

internal class MethodFactory
{
    private readonly ReturnValueFactory _returnValueFactory;
    private readonly ParameterListFactory _parameterListFactory;

    public MethodFactory(ReturnValueFactory returnValueFactory, ParameterListFactory parameterListFactory)
    {
        _returnValueFactory = returnValueFactory;
        _parameterListFactory = parameterListFactory;
    }

    public Method Create(Input.Method method)
    {
        if (method.Name is null)
            throw new Exception("Methodinfo name is null");

        if (method.ReturnValue is null)
            throw new Exception($"{nameof(Input.Method)} {method.Name} {nameof(method.ReturnValue)} is null");

        if (method.Identifier is null)
            throw new Exception($"{nameof(Input.Method)} {method.Name} is missing {nameof(method.Identifier)} value");

        if (method.Name != string.Empty)
        {
            return new Method(
                identifier: method.Identifier,
                name: method.Name,
                returnValue: _returnValueFactory.Create(method.ReturnValue),
                parameterList: _parameterListFactory.Create(method.Parameters),
                throws: method.Throws,
                introspectable: method.Introspectable,
                getProperty: PropertyReference.Create(method.GetProperty),
                setProperty: PropertyReference.Create(method.SetProperty),
                version: method.Version
            );
        }

        if (!string.IsNullOrEmpty(method.MovedTo))
            throw new MethodMovedException(method, $"Method {method.Identifier} moved to {method.MovedTo}.");

        throw new Exception($"{nameof(Input.Method)} {method.Identifier} has no {nameof(method.Name)} and did not move.");

    }

    public IEnumerable<Method> Create(IEnumerable<Input.Method> methods)
    {
        var list = new List<Method>();

        foreach (var method in methods)
        {
            try
            {
                list.Add(Create(method));
            }
            catch (SingleParameterFactory.VarArgsNotSupportedException ex)
            {
                Log.Verbose($"Method {method.Name} could not be created: {ex.Message}");
            }
            catch (MethodMovedException ex)
            {
                Log.Verbose($"Method ignored: {ex.Message}");
            }
        }

        return list;
    }

    public class MethodMovedException : Exception
    {
        public Input.Method Method { get; }

        public MethodMovedException(Input.Method method, string message) : base(message)
        {
            Method = method;
        }
    }
}
