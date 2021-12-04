using System;
using System.Text;

namespace Generator3.Renderer.Public
{
    public static class Property
    {
        public static string Render(this Model.Public.Property property)
        {
            try
            {
                ThrowIfNotSupported(property);
                
                var builder = new StringBuilder();
                builder.AppendLine(property.RenderDescriptor());
                builder.AppendLine(property.RenderAccessor());

                return builder.ToString();
            }
            catch (Exception ex)
            {
                Log.Warning($"Did not generate property '{property.ClassName}.{property.NativeName}': {ex.Message}");
                return string.Empty;
            }
        }

        //TODO: Remove this method if all cases are supported
        private static void ThrowIfNotSupported(Model.Public.Property property)
        {
            if (property.IsPrimitiveType)
                return;

            throw new Exception($"Property {property.ClassName}.{property.ManagedName} is not supported");
        }
    }
}
