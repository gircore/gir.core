namespace Generator3.Converter
{
    public static class FieldNameExtension
    {
        public static string GetInternalName(this GirModel.Field field)
        {
            return field.Name.ToPascalCase();
        }
    }
}
