namespace Generator.Renderer.Public.InstanceParameterToNativeExpressions;

public class Interface : InstanceParameterConverter
{
    public bool Supports(GirModel.Type type)
    {
        return type is GirModel.Interface;
    }

    public string GetExpression(GirModel.InstanceParameter instanceParameter)
    {
        return "this.Handle.DangerousGetHandle()";
    }
}
