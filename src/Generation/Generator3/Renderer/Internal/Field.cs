namespace Generator3.Renderer.Internal
{
    public static class Field
    {
        public static string Render(this Model.Internal.Field field)
        {
            return @$"{field.Attribute.Render()}public {field.NullableTypeName} {field.Name};";
        }
    }
}
