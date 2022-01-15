namespace Generator3.Converter
{
    public static class RecordNameConverter
    {
        public static string GetFullyQualifiedInternalStructName(this GirModel.Record record)
            => record.Namespace.GetInternalName() + "." + GetInternalStructName(record);

        public static string GetFullyQualifiedInternalHandle(this GirModel.Record record)
            => record.Namespace.GetInternalName() + "." + GetInternalHandleName(record);

        public static string GetFullyQualifiedInternalNullHandle(this GirModel.Record record)
            => GetFullyQualifiedInternalHandle(record) + ".Null";

        public static string GetFullyQualifiedInternalManagedHandleCreateMethod(this GirModel.Record record)
            => record.Namespace.GetInternalName() + "." + GetInternalManagedHandleName(record) + ".Create";

        public static string GetFullyQualifiedPublicClassName(this GirModel.Record record)
            => record.Namespace.Name + "." + record.GetPublicClassName();

        public static string GetPublicClassName(this GirModel.Record record)
            => record.Name;

        public static string GetInternalStructName(this GirModel.Record record)
            => record.Name + "Data";

        public static string GetInternalHandleName(this GirModel.Record record)
            => record.Name + "Handle";

        public static string GetInternalManagedHandleName(this GirModel.Record record)
            => record.Name + "ManagedHandle";
    }
}
