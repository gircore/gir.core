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

        if (cls.Fundamental)
            return "this.Handle";

        return cls.Parent is null
            ? "this.Handle.DangerousGetHandle()"
            : "base.Handle.DangerousGetHandle()";
    }
}
