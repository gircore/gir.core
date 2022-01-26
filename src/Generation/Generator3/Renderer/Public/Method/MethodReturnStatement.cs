using Generator3.Converter;

namespace Generator3.Renderer.Public
{
    public static class MethodReturnStatement
    {
        public static string Render(Model.Public.Method method, string returnVariable)
        {
            return method.ReturnType.AnyType.Is<GirModel.Void>()
                ? string.Empty
                : $"return {method.ReturnType.ToManaged(returnVariable)};";
        }
    }
}
