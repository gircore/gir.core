namespace Generator.Renderer.Public.Parameter;

internal class PrimitiveValueTypeArray : ParameterConverter
{
    public bool Supports(GirModel.AnyType anyType)
    {
        return anyType.IsArray<GirModel.PrimitiveValueType>();
    }

    public ParameterTypeData Create(GirModel.Parameter parameter)
    {
        return parameter switch
        {
            { Direction: GirModel.Direction.Out, CallerAllocates: true } => OutCallerAllocates(parameter),
            { Direction: GirModel.Direction.In } => In(parameter),
            { Direction: GirModel.Direction.InOut } => InOut(parameter),
            _ => throw new System.Exception("Unsupported byte array type")
        };
    }

    private static ParameterTypeData InOut(GirModel.Parameter parameter)
    {
        var type = Model.Type.GetName(parameter.AnyTypeOrVarArgs.AsT0.AsT1.AnyType.AsT0);

        return new ParameterTypeData(
            Direction: ParameterDirection.In(),
            NullableTypeName: $"Span<{type}>"
        );
    }

    private static ParameterTypeData OutCallerAllocates(GirModel.Parameter parameter)
    {
        var type = Model.Type.GetName(parameter.AnyTypeOrVarArgs.AsT0.AsT1.AnyType.AsT0);

        return new ParameterTypeData(
            Direction: ParameterDirection.In(),
            NullableTypeName: $"Span<{type}>"
        );
    }

    private static ParameterTypeData In(GirModel.Parameter parameter)
    {
        var type = Model.Type.GetName(parameter.AnyTypeOrVarArgs.AsT0.AsT1.AnyType.AsT0);

        if (parameter.AnyTypeOrVarArgs.AsT0.AsT1.Length is not null)
        {
            return new ParameterTypeData(
                Direction: ParameterDirection.In(),
                NullableTypeName: $"Span<{type}>"
            );
        }
        else
        {
            return new ParameterTypeData(
                Direction: ParameterDirection.In(),
                NullableTypeName: $"ref {type}"
            );
        }
    }
}
