namespace Generator.Renderer.Internal.Parameter;

public class PointerGLibPtrArray : ParameterConverter
{
    public bool Supports(GirModel.AnyType anyType)
    {
        return anyType.IsGLibPtrArray();
    }

    public RenderableParameter Convert(GirModel.Parameter parameter)
    {
        return new RenderableParameter(
            Attribute: string.Empty,
            Direction: string.Empty,
            NullableTypeName: Model.Type.Pointer,
            Name: Model.Parameter.GetName(parameter)
        );
    }
}

