namespace Generator.Renderer.Public.InstanceParameterToNativeExpressions;

internal interface InstanceParameterConverter
{
    bool Supports(GirModel.Type type);
    string GetExpression(GirModel.InstanceParameter instanceParameter);
}
