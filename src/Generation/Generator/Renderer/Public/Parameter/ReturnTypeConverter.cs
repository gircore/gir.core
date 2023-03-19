namespace Generator.Renderer.Public.Parameter;

internal interface ParameterConverter
{
    bool Supports(GirModel.AnyType anyType);
    ParameterTypeData Create(GirModel.Parameter parameter);
}
