namespace Generator.Renderer.Public.Constructor;

public class OpaqueUntypedRecord : ConstructorConverter
{
    public bool Supports(GirModel.Constructor constructor)
    {
        return constructor.Parent is GirModel.Record record && Model.Record.IsOpaqueUntyped(record);
    }

    public ConstructorData GetData(GirModel.Constructor constructor)
    {
        return new(
            RequiresNewModifier: false,
            GetCreateExpression: CreateExpression,

            //Constructors which do not transfer ownership likely create floating references.
            //As memory management is implemented manually for untyped records the handles
            //need to make sure that references are sunk.
            AllowRendering: true
        );
    }

    private static string CreateExpression(GirModel.Constructor constructor, string fromVariableName)
    {
        var own = constructor.ReturnType.Transfer == GirModel.Transfer.None
            ? ".OwnedCopy()"
            : string.Empty;

        var record = (GirModel.Record) constructor.Parent;
        var createInstance = $"new {record.Name}({fromVariableName}{own})";

        return constructor.ReturnType.Nullable
            ? $"{fromVariableName}.IsInvalid ? null : {createInstance}"
            : createInstance;
    }
}
