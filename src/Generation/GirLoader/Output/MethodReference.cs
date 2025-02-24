using System;

namespace GirLoader.Output;

public class MethodReference
{
    private Method? _method;

    public string Name { get; }
    public Method? Method => _method; //Workaround: Remove property if AccessorResolver gets fixed

    public MethodReference(string name)
    {
        Name = name;
    }

    public Method GetMethod()
        => _method ?? throw new Exception($"Method {Name} is not resolved");

    public void ResolveMethod(Method? method)
    {
        _method = method;
    }

    public static MethodReference? Create(string? name)
    {
        return name is null
            ? null
            : new MethodReference(name);
    }
}
