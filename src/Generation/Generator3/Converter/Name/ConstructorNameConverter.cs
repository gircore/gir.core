namespace Generator3.Renderer.Converter
{
    public static class ConstructorNameConverter
    {
        public static string GetInternalName(this GirModel.Constructor constructor)
        {
            return constructor.Name.ToPascalCase().EscapeIdentifier();
        }
    }
}
