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

        return cls.Fundamental
            ? $"new {cls.Name}({fromVariableName})"
            : $"CreateInstance({fromVariableName})";
    }
}
