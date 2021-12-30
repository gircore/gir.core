using System;

namespace Generator3.Renderer.Internal
{
    public static class Callback
    {
        public static string RenderWithAttributes(this Model.Internal.Callback callback)
        {
            try
            {
                return Render(callback);
            }
            catch (Exception ex)
            {
                Log.Warning($"Could not render internal callback: {callback.Name}: {ex.Message}");
                return string.Empty;
            }
        }

        private static string Render(Model.Internal.Callback callback)
        {
            return $"public delegate {callback.ReturnType.NullableTypeName} {callback.Name}({callback.Parameters.Render()});";
        }
    }
}
