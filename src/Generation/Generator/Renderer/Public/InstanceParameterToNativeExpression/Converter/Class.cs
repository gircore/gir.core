namespace Generator.Renderer.Public.InstanceParameterToNativeExpressions;

public class Class : InstanceParameterConverter
{
    public bool Supports(GirModel.Type type)
    {
        return type is GirModel.Class;
    }

    public string GetExpression(GirModel.InstanceParameter instanceParameter)
    {
        return "this.Handle";
    }
}
