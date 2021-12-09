namespace Generator3.Converter
{
    public static class PropertyNameConverter
    {
        public static string GetPublicName(this GirModel.Property property)
        {
            return property.Name.ToPascalCase();
        }
    }
}
