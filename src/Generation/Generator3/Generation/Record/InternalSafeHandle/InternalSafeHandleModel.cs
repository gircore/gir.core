namespace Generator3.Generation.Record
{
    public class InternalSafeHandleModel
    {
        public string Name => Record.Name;
        public string HandleClassName => "Handle";
        public string InternalNamespaceName => Record.Namespace.GetInternalName();
        public string NamespaceName => Record.Namespace.Name;
        public GirModel.Record Record { get; }
        public GirModel.Method? FreeMethod => Record.Methods.GetFreeOrUnrefMethod();

        public InternalSafeHandleModel(GirModel.Record record)
        {
            Record = record;
        }
    }
}
