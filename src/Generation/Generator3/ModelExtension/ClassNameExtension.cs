namespace Generator3.Converter
{
    public static class ClassNameExtension
    {
        public static string GetFullyQualifiedInternalStructName(this GirModel.Class @class)
            => @class.Namespace.GetInternalName() + "." + GetInternalStructName(@class);

        public static string GetInternalStructName(this GirModel.Class @class)
            => @class.Name + "Data";
    }
}
