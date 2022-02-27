namespace Generator3.Converter
{
    public static class RecordNameExtension
    {
        public static string GetFullyQualifiedInternalStructName(this GirModel.Record record)
            => record.Namespace.GetInternalName() + "." + GetInternalStructName(record);

        public static string GetFullyQualifiedInternalHandle(this GirModel.Record record)
            => record.Namespace.GetInternalName() + "." + GetInternalHandleName(record);

        public static string GetFullyQualifiedInternalNullHandleInstance(this GirModel.Record record)
            => record.Namespace.GetInternalName() + "." + GetInternalNullHandleName(record) + "." + "Instance";

        public static string GetFullyQualifiedInternalOwnedHandle(this GirModel.Record record)
            => record.Namespace.GetInternalName() + "." + GetInternalOwnedHandleName(record);

        public static string GetFullyQualifiedInternalUnownedHandle(this GirModel.Record record)
            => record.Namespace.GetInternalName() + "." + GetInternalUnownedHandleName(record);

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

        public static string GetInternalNullHandleName(this GirModel.Record record)
            => record.Name + "NullHandle";

        public static string GetInternalOwnedHandleName(this GirModel.Record record)
            => record.Name + "OwnedHandle";

        public static string GetInternalUnownedHandleName(this GirModel.Record record)
            => record.Name + "UnownedHandle";

        public static string GetInternalManagedHandleName(this GirModel.Record record)
            => record.Name + "ManagedHandle";
    }
}
