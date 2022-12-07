using GirModel;

namespace Generator.Renderer.Public.ReturnTypeToManagedExpressions;

internal interface ReturnTypeConverter
{
    bool Supports(AnyType type);
    string GetString(GirModel.ReturnType returnType, string fromVariableName);
}
