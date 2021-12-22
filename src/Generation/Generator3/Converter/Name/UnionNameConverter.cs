namespace Generator3.Converter
{
    public static class UnionNameConverter
    {
        public static string GetFullyQualifiedInternalStruct(this GirModel.Union union)
            => union.Namespace.GetInternalName() + "." + union.GetName() + ".Struct";
    }
}
