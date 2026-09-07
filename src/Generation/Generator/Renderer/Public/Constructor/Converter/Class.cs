namespace Generator.Renderer.Public.Constructor;

public class Class : ConstructorConverter
{
    public bool Supports(GirModel.Constructor constructor)
    {
        return constructor.Parent is GirModel.Class;
    }

    public ConstructorData GetData(GirModel.Constructor constructor)
    {
        var parentClass = ((GirModel.Class) constructor.Parent).Parent;
        return new(
            RequiresNewModifier: Model.Class.HidesConstructor(parentClass, constructor),
            GetCreateExpression: CreateExpression,
            AllowRendering: true
        );
    }

    private static string CreateExpression(GirModel.Constructor constructor, string fromVariableName)
    {
        var cls = (GirModel.Class) constructor.Parent;

        return (cls.Fundamental, constructor.ReturnType.Nullable) switch
        {
            (true, true) => $"{fromVariableName} == global::System.IntPtr.Zero ? null : new {cls.Name}({fromVariableName})",
            (true, false) => $"new {cls.Name}({fromVariableName})",
            (false, true) => $"{fromVariableName} == global::System.IntPtr.Zero ? null : CreateInstance({fromVariableName})",
            (false, false) => $"CreateInstance({fromVariableName})",
        };
    }
}
