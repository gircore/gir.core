using System;

namespace Generator.Renderer.Public;

public class ReturnTypeToManagedData(GirModel.ReturnType returnType)
{
    private Func<string, string>? _expression;
    private Func<string, string>? _postReturnStatement;

    public GirModel.ReturnType ReturnType { get; } = returnType;

    public void SetPostReturnStatement(Func<string, string>? expression)
    {
        _postReturnStatement = expression;
    }

    public string? GetPostReturnStatement(string returnVariable)
    {
        return _postReturnStatement?.Invoke(returnVariable);
    }

    public void SetExpression(Func<string, string> expression)
    {
        _expression = expression;
    }

    public string? GetExpression(string returnVariable)
    {
        return _expression?.Invoke(returnVariable);
    }
}
