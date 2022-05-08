using GirModel;

namespace Generator.Renderer.Public.ReturnTypeExpression;

internal interface ReturnTypeConverter
{
    bool Supports(AnyType type);
    string GetString(GirModel.ReturnType returnType, string fromVariableName);
}
