using GirModel;

namespace Generator.Renderer.Public.ReturnTypeToManagedExpressions;

internal class Bitfield : ReturnTypeConverter
{
    public bool Supports(AnyType type)
        => type.Is<GirModel.Bitfield>();

    public string GetString(GirModel.ReturnType returnType, string fromVariableName)
        => fromVariableName;
}
