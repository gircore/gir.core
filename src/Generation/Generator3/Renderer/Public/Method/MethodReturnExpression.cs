using Generator3.Converter;

namespace Generator3.Renderer.Public
{
    public static class MethodReturnExpression
    {
        public static string RenderPublicMethodReturnExpression(this GirModel.ReturnType data)
        {
            return data.AnyType.Is<GirModel.Void>()
                ? string.Empty 
                : $"return {data.ToPublicFromVariable()};";
        }
    }
}
