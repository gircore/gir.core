namespace Generator3.Converter
{
    public static class ConstructorNameExtension
    {
        public static string GetInternalName(this GirModel.Constructor constructor)
        {
            return constructor.Name.ToPascalCase().EscapeIdentifier();
        }

        public static string GetPublicName(this GirModel.Constructor constructor)
        {
            return constructor.Name.ToPascalCase().EscapeIdentifier();
        }
    }
}
