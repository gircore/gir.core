using Generator3.Converter;

namespace Generator3.Renderer.Public
{
    public static class MethodReturnStatement
    {
        public static string Render(Model.Public.Method data, string returnVariable)
        {
            return data.ReturnType.AnyType.Is<GirModel.Void>()
                ? string.Empty 
                : $"return {data.ReturnType.ToManaged(returnVariable)};";
        }
    }
}
