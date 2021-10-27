namespace Generator3.Renderer
{
    public static class NativeField
    {
        public static string Render(this Model.Field field)
        {
            return @$"{field.Attribute.IfNotNullAppendNewline()}public {field.NullableTypeName} {field.Name};";
        }
    }
}
