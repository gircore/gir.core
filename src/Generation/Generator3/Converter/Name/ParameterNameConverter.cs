namespace Generator3.Converter
{
    public static class ParameterNameConverter
    {
        public static string GetInternalName(this GirModel.Parameter parameter)
        {
            return parameter.Name.ToCamelCase().EscapeIdentifier();
        }

        public static string GetPublicName(this GirModel.Parameter parameter)
        {
            return parameter.Name.ToCamelCase().EscapeIdentifier();
        }
    }
}
