namespace Generator3.Renderer
{
    public static class Field
    {
        public static string Render(this Model.Field field)
        {
            return @$"{field.Attribute.Render()}public {field.NullableTypeName} {field.Name};";
        }
    }
}
