namespace Generator3.Generation.Framework
{
    public class ModuleDllImportModel
    {
        public string NamespaceName { get; }

        public ModuleDllImportModel(string @namespace)
        {
            this.NamespaceName = @namespace;
        }
    }
}
