namespace Generator.Renderer.Internal.Parameter;

public class PrimitiveValueTypeArray : ParameterConverter
{
    public bool Supports(GirModel.AnyType anyType)
    {
        return anyType.IsArray<GirModel.PrimitiveValueType>();
    }

    public RenderableParameter Convert(GirModel.Parameter parameter)
    {
        return parameter switch
        {
            { Direction: GirModel.Direction.In } => In(parameter),
            { Direction: GirModel.Direction.Out, CallerAllocates: true } => OutCallerAllocates(parameter),
            { Direction: GirModel.Direction.Out } => Out(parameter),
            { Direction: GirModel.Direction.InOut } => InOut(parameter),
            _ => throw new System.Exception($"Unknown direction {parameter.Direction}")
        };
    }

    private static RenderableParameter In(GirModel.Parameter parameter)
    {
        return new RenderableParameter(
            Attribute: string.Empty,
            Direction: ParameterDirection.Ref(),
            NullableTypeName: Model.Type.GetName(parameter.AnyTypeOrVarArgs.AsT0.AsT1.AnyType.AsT0),
            Name: Model.Parameter.GetName(parameter)
        );
    }

    private static RenderableParameter Out(GirModel.Parameter parameter)
    {
        return new RenderableParameter(
            Attribute: string.Empty,
            Direction: ParameterDirection.Out(),
            NullableTypeName: Model.ArrayType.GetName(parameter.AnyTypeOrVarArgs.AsT0.AsT1),
            Name: Model.Parameter.GetName(parameter)
        );
    }

    private static RenderableParameter OutCallerAllocates(GirModel.Parameter parameter)
    {
        return new RenderableParameter(
            Attribute: string.Empty,
            Direction: ParameterDirection.Ref(),
            NullableTypeName: Model.Type.GetName(parameter.AnyTypeOrVarArgs.AsT0.AsT1.AnyType.AsT0),
            Name: Model.Parameter.GetName(parameter)
        );
    }

    private static RenderableParameter InOut(GirModel.Parameter parameter)
    {
        return new RenderableParameter(
            Attribute: string.Empty,
            Direction: ParameterDirection.Ref(),
            NullableTypeName: Model.Type.GetName(parameter.AnyTypeOrVarArgs.AsT0.AsT1.AnyType.AsT0),
            Name: Model.Parameter.GetName(parameter)
        );
    }
}

