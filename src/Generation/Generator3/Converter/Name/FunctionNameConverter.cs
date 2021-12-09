namespace Generator3.Renderer.Converter
{
    public static class FunctionNameConverter
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
