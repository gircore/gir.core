namespace Generator3.Converter
{
    public static class RecordNameConverter
    {
        public static string GetFullyQualifiedInternalStruct(this GirModel.Record record)
            => record.Namespace.GetInternalName() + "." + record.GetName() + ".Struct";

        public static string GetFullyQualifiedInternalHandle(this GirModel.Record record)
            => record.Namespace.GetInternalName() + "." + record.GetName() + ".Handle";

        public static string GetFullyQualifiedInternalNullHandle(this GirModel.Record record)
            => GetFullyQualifiedInternalHandle(record) + ".Null";

        public static string GetFullyQualifiedInternalManagedHandleCreateMethod(this GirModel.Record record)
            => record.Namespace.GetInternalName() + "." + record.GetName() + ".ManagedHandle.Create";

        public static string GetFullyQualifiedPublicClassName(this GirModel.Record record)
            => record.Namespace.Name + "." + record.GetPublicClassName();

        public static string GetPublicClassName(this GirModel.Record record)
            => record.Name;
    }
}
