using Generator3.Converter;

namespace Generator3.Generation.Callback
{
    public static class PublicHandlerReturnStatement
    {
        public static string Render(PublicHandlerModel model, string returnVariableName)
        {
            return model.InternalReturnType.Model.AnyType.Is<GirModel.Void>()
                ? string.Empty
                : $"return {model.InternalReturnType.Model.ToNative(returnVariableName)};";
        }
    }
}
