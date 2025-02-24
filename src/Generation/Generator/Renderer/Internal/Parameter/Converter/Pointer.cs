using System;

namespace Generator.Renderer.Internal.Parameter;

internal class Pointer : ParameterConverter
{

    public bool Supports(GirModel.AnyType anyType)
    {
        return anyType.Is<GirModel.Pointer>();
    }

    public RenderableParameter Convert(GirModel.Parameter parameter)
    {
        return parameter.Direction switch
        {
            GirModel.Direction.In => In(parameter),
            GirModel.Direction.Out => Out(parameter),
            GirModel.Direction.InOut => Ref(parameter),
            _ => throw new Exception("Unknown internal parameter direction for pointer")
        };
    }

    private static RenderableParameter In(GirModel.Parameter parameter)
    {
        //IntPtr can't be nullable. They can be "nulled" via IntPtr.Zero.
        return new RenderableParameter(
            Attribute: string.Empty,
            Direction: string.Empty,
            NullableTypeName: Model.Type.GetName(parameter.AnyTypeOrVarArgs.AsT0.AsT0),
            Name: Model.Parameter.GetName(parameter)
        );
    }

    private static RenderableParameter Out(GirModel.Parameter parameter)
    {
        //IntPtr can't be nullable. They can be "nulled" via IntPtr.Zero.
        return new RenderableParameter(
            Attribute: string.Empty,
            Direction: ParameterDirection.Out(),
            NullableTypeName: Model.Type.GetName(parameter.AnyTypeOrVarArgs.AsT0.AsT0),
            Name: Model.Parameter.GetName(parameter)
        );
    }

    private static RenderableParameter Ref(GirModel.Parameter parameter)
    {
        //IntPtr can't be nullable. They can be "nulled" via IntPtr.Zero.
        return new RenderableParameter(
            Attribute: string.Empty,
            Direction: ParameterDirection.Ref(),
            NullableTypeName: Model.Type.GetName(parameter.AnyTypeOrVarArgs.AsT0.AsT0),
            Name: Model.Parameter.GetName(parameter)
        );
    }
}
