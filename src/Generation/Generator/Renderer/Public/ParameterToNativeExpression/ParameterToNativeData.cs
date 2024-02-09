using System;

namespace Generator.Renderer.Public;

public class ParameterToNativeData
{
    private Func<string>? _getCallName;
    private Func<string>? _getSignatureName;
    private Func<string>? _getExpression;
    private Func<string>? _getPostCallExpression;

    private string? _callName;
    private string? _signatureName;
    private string? _expression;
    private string? _postCallExpression;

    public GirModel.Parameter Parameter { get; }

    public bool IsCallbackUserData { get; internal set; }
    public bool IsDestroyNotify { get; internal set; }
    public bool IsArrayLengthParameter { get; internal set; }
    public bool IsInOutArrayLengthParameter { get; internal set; }
    public bool IsGLibErrorParameter { get; internal set; }

    public ParameterToNativeData(GirModel.Parameter parameter)
    {
        Parameter = parameter;
    }

    public void SetExpression(Func<string> getExpression)
    {
        _getExpression = getExpression;
    }

    public string? GetExpression() => _expression ??= _getExpression?.Invoke();

    public void SetPostCallExpression(Func<string> getPostCallExpression)
    {
        _getPostCallExpression = getPostCallExpression;
    }

    public string? GetPostCallExpression() => _postCallExpression ??= _getPostCallExpression?.Invoke();

    public void SetCallName(Func<string> getCallName)
    {
        _getCallName = getCallName;
    }

    public string GetCallName()
    {
        _callName ??= _getCallName?.Invoke();
        return _callName ?? throw new Exception($"Callname of parameter {Parameter.Name} ({Parameter.AnyTypeOrVarArgs} is not set");
    }

    public void SetSignatureName(Func<string> getSignatureName)
    {
        _getSignatureName = getSignatureName;
    }

    public string GetSignatureName()
    {
        _signatureName ??= _getSignatureName?.Invoke();
        return _signatureName ?? throw new Exception($"Signaturename of parameter {Parameter.Name} ({Parameter.AnyTypeOrVarArgs} is not set");
    }
}
