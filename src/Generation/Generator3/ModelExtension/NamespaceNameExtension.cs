namespace Generator3.Converter
{
    public static class NamespaceNameExtension
    {
        public static string GetPublicName(this GirModel.Namespace @namespace)
            => @namespace.Name.ToPascalCase().EscapeIdentifier();

        public static string GetCanonicalName(this GirModel.Namespace @namespace)
            => $"{@namespace.Name}-{@namespace.Version}";

        public static string GetInternalName(this GirModel.Namespace @namespace)
            => $"{@namespace.GetPublicName()}.Internal";
    }
}
