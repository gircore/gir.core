namespace Generator.Renderer.Public.Parameter;

internal interface ParameterConverter
{
    bool Supports(GirModel.AnyTypeReference anyTypeReference);
    ParameterTypeData Create(GirModel.Parameter parameter);
}
