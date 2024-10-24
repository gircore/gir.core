namespace Generator.Renderer.Public.InstanceParameterToNativeExpressions;

public class Class : InstanceParameterConverter
{
    public bool Supports(GirModel.Type type)
    {
        return type is GirModel.Class;
    }

    public string GetExpression(GirModel.InstanceParameter instanceParameter)
    {
        var cls = (GirModel.Class) instanceParameter.Type;

        return cls.Fundamental 
            ? "this.Handle" 
            : "this.Handle.DangerousGetHandle()";
    }
}
