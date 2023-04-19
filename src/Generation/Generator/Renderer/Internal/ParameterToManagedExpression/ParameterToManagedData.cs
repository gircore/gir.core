using System;

namespace Generator.Renderer.Internal;

public class ParameterToManagedData
{
    private string? _callName;
    private string? _signatureName;
    private string? _expression;
    private string? _postCallExpression;

    public GirModel.Parameter Parameter { get; }

    public bool IsCallbackUserData { get; internal set; }
    public bool IsCallbackDestroyNotify { get; internal set; }
    public bool IsArrayLengthParameter { get; internal set; }
    public bool HasCallName => _callName is not null;
    public bool HasSignatureName => _signatureName is not null;

    public ParameterToManagedData(GirModel.Parameter parameter)
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
        return _callName ?? throw new Exception($"Callname of parameter {Parameter.Name} ({Parameter.AnyTypeOrVarArgs} is not set");
    }

    public void SetSignatureName(string signatureName)
    {
        _signatureName = signatureName;
    }

    public string GetSignatureName()
    {
        return _signatureName ?? throw new Exception($"Signaturename of parameter {Parameter.Name} ({Parameter.AnyTypeOrVarArgs} is not set");
    }
}
