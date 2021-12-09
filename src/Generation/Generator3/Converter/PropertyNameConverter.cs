namespace Generator3.Renderer.Converter
{
    public static class PropertyNameConverter
    {
        public static string GetPublicName(this GirModel.Property property)
        {
            return property.Name.ToPascalCase();
        }
    }
}
