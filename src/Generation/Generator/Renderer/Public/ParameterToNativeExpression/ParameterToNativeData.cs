using System;
using GirModel;

namespace Generator.Renderer.Public;

public class ParameterToNativeData
{
    private string? _callName;
    private string? _signatureName;
    private string? _expression;
    private string? _postCallExpression;

    public Parameter Parameter { get; }

    public bool IsClosure { get; internal set; }
    public bool IsDestroyNotify { get; internal set; }
    public bool HasCallName => _callName is not null;
    public bool HasSignatureName => _signatureName is not null;

    public ParameterToNativeData(GirModel.Parameter parameter)
    {
        Parameter = parameter;
    }

    public void SetExpression(string expression)
    {
        _expression = expression;
    }

    public string? GetExpression() => _expression;

    public void SetPostCallExpression(string expression)
    {
        _postCallExpression = expression;
    }

    public string? GetPostCallExpression() => _postCallExpression;

    public void SetCallName(string name)
    {
        _callName = name;
    }

    public string GetCallName()
    {
        return _callName ?? throw new Exception($"Callname of parameter {Parameter.Name} ({Parameter.AnyType} is not set");
    }

    public void SetSignatureName(string signatureName)
    {
        _signatureName = signatureName;
    }

    public string GetSignatureName()
    {
        return _signatureName ?? throw new Exception($"Signaturename of parameter {Parameter.Name} ({Parameter.AnyType} is not set");
    }
}
