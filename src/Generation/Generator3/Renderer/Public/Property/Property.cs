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
                var message = $"Did not generate property '{property.ClassName}.{property.NativeName}': {ex.Message}";
                
                if(ex is NotImplementedException)
                    Log.Information(message);
                else
                    Log.Warning(message);

                return string.Empty;
            }
        }

        private static void ThrowIfNotSupported(Model.Public.Property property)
        {
            if (property.AnyType.Is<GirModel.PrimitiveType>())
                return;

            if (property.AnyType.IsArray<GirModel.String>())
                return;

            if (property.AnyType.IsArray<GirModel.Byte>())
                return;
            
            if(property.AnyType.Is<GirModel.Enumeration>())
                return;
            
            if(property.AnyType.Is<GirModel.Bitfield>())
                return;
            
            if(property.AnyType.Is<GirModel.Class>())
                return;
            
            if(property.AnyType.Is<GirModel.Interface>())
                return;

            if (property.AnyType.Is<GirModel.Record>())
                throw new NotImplementedException("There is currently no concept for transfering native records (structs) into the managed world.");

            throw new Exception($"Property {property.ClassName}.{property.PublicName} is not supported");
        }
    }
}
