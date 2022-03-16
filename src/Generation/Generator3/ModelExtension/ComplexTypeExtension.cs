namespace Generator3.Converter
{
    public static class ComplexTypeExtension
    {
        public static string GetFullyQualified(this GirModel.ComplexType type)
            => type.Namespace.GetPublicName() + "." + type.Name;
    }
}
