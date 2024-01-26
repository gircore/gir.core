using System;

namespace Generator.Renderer.Public.InstanceParameterToNativeExpressions;

public class UntypedRecord : InstanceParameterConverter
{
    public bool Supports(GirModel.Type type)
    {
        return type is GirModel.Record record && Model.Record.IsUntyped(record);
    }

    public string GetExpression(GirModel.InstanceParameter instanceParameter)
    {
        return instanceParameter switch
        {
            { Transfer: GirModel.Transfer.None } => "this.Handle",
            _ => throw new Exception("Unknown transfer type for untyped record instance parameter")
        };
    }
}
