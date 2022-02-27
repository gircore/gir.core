namespace Generator3.Converter
{
    public static class PropertyNameExtension
    {
        public static string GetPublicName(this GirModel.Property property)
        {
            return property.Name.ToPascalCase();
        }
    }
}
