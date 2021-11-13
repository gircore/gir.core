namespace Generator3.Renderer.Native
{
    public static class Field
    {
        public static string Render(this Model.Native.Field field)
        {
            return @$"{field.Attribute.Render()}public {field.NullableTypeName} {field.Name};";
        }
    }
}
