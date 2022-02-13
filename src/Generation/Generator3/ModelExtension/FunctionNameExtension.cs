namespace Generator3.Converter
{
    public static class FunctionNameExtension
    {
        public static string GetInternalName(this GirModel.Function function)
        {
            return function.Name.ToPascalCase().EscapeIdentifier();
        }

        public static string GetPublicName(this GirModel.Function function)
        {
            return function.Name.ToPascalCase().EscapeIdentifier();
        }
    }
}
