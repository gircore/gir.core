using System;

namespace Generator.Renderer.Public.Signals;

public class SignalArgsParameterData(GirModel.Parameter parameter)
{
    private Func<string>? _getExpression;
    private string? _expression;

    public GirModel.Parameter Parameter { get; } = parameter;
    public bool IsArrayLengthParameter { get; internal set; }

    public void SetExpression(Func<string> getExpression)
    {
        _getExpression = getExpression;
    }

    public string? GetExpression() => _expression ??= _getExpression?.Invoke();
}
