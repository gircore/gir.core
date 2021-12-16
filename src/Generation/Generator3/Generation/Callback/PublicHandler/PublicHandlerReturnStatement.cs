using System;
using Generator3.Converter;

namespace Generator3.Generation.Callback
{
    public static class PublicHandlerReturnStatement
    {
        public static string Render(PublicHandlerModel model, string returnVariableName)
        {
            try
            {
                return model.InternalReturnType.Model.AnyType.Is<GirModel.Void>()
                    ? string.Empty
                    : $"return {model.InternalReturnType.Model.ToManaged(returnVariableName)};";
            }
            catch (Exception ex)
            {
                Log.Warning($"{model.Name}: Could not create public handler return statement: {ex.Message}");
                return string.Empty;
            }
        }
    }
}
