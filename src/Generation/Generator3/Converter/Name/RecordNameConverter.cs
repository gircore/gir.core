namespace Generator3.Converter
{
    public static class RecordNameConverter
    {
        public static string GetFullyQualifiedInternalStruct(this GirModel.Record record)
            => record.Namespace.GetInternalName() + "." + record.GetName() + ".Struct";

        public static string GetFullyQualifiedInternalHandle(this GirModel.Record record)
            => record.Namespace.GetInternalName() + "." + record.GetName() + ".Handle";
    }
}
