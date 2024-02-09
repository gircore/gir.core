using System;

namespace Generator.Renderer.Internal;

public class ParameterToManagedData
{
    private Func<string>? _getCallName;
    private Func<string>? _getSignature;
    private Func<string>? _getExpression;
    private Func<string>? _getPostCallExpression;

    private string? _callName;
    private string? _signatureName;
    private string? _expression;
    private string? _postCallExpression;

    public GirModel.Parameter Parameter { get; }

    public bool IsCallbackUserData { get; internal set; }
    public bool IsCallbackDestroyNotify { get; internal set; }
    public bool IsArrayLengthParameter { get; internal set; }
    public bool IsGLibErrorParameter { get; internal set; }

    public ParameterToManagedData(GirModel.Parameter parameter)
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
        return _callName ?? throw new Exception($"Callname of parameter {Parameter.Name} ({Parameter.AnyTypeOrVarArgs} is not set while converting to managed");
    }

    public void SetSignatureName(Func<string> getSignatureName)
    {
        _getSignature = getSignatureName;
    }

    public string GetSignatureName()
    {
        _signatureName ??= _getSignature?.Invoke();
        return _signatureName ?? throw new Exception($"Signaturename of parameter {Parameter.Name} ({Parameter.AnyTypeOrVarArgs} is not set while converting to managed");
    }
}
