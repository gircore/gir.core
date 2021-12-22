namespace Generator3.Converter
{
    public static class ClassNameConverter
    {
        public static string GetFullyQualifiedInternalStruct(this GirModel.Class @class)
            => @class.Namespace.GetInternalName() + "." + @class.GetName() + ".Instance.Struct";
    }
}
