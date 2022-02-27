namespace Generator3.Converter
{
    public static class UnionNameExtension
    {
        public static string GetFullyQualifiedInternalStructName(this GirModel.Union union)
            => union.Namespace.GetInternalName() + "." + GetInternalStructName(union);

        public static string GetInternalStructName(this GirModel.Union union)
            => union.Name + "Data";
    }
}
