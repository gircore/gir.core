namespace Generator.Renderer.Public.Constructor;

public class TypedRecord : ConstructorConverter
{
    public bool Supports(GirModel.Constructor constructor)
    {
        return constructor.Parent is GirModel.Record record && Model.Record.IsTyped(record);
    }

    public ConstructorData GetData(GirModel.Constructor constructor)
    {
        return new(
            RequiresNewModifier: false,
            GetCreateExpression: CreateExpression,

            //Constructors which do not transfer ownership likely create floating references.
            //as there is no way to know how to sink those references those constructors are not rendered
            //automatically as part of the public api and must be implemented manually.
            AllowRendering: constructor.ReturnType.Transfer == GirModel.Transfer.Full
        );
    }

    private static string CreateExpression(GirModel.Constructor constructor, string fromVariableName)
    {
        var record = (GirModel.Record) constructor.Parent;
        var createInstance = $"new {record.Name}({fromVariableName})";

        return constructor.ReturnType.Nullable
            ? $"{fromVariableName}.IsInvalid ? null : {createInstance}"
            : createInstance;
    }
}
