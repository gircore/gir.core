namespace Generator.Renderer.Public;

public class CallableExpressions
{
    public static CallableData Initialize(GirModel.Callable callable)
    {
        var parameters = ParameterToNativeExpression.Initialize(callable.Parameters);
        var returnType = ReturnTypeToManagedExpression.Initialize(callable.ReturnType, parameters);

        return new CallableData(returnType, parameters);
    }
}
