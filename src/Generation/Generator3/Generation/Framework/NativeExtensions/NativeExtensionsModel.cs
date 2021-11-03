namespace Generator3.Generation.Framework
{
    public class NativeExtensionsModel
    {
        private readonly string _namespace;

        public string NamespaceName => _namespace + ".Native";

        public NativeExtensionsModel(string @namespace)
        {
            this._namespace = @namespace;
        }
    }
}
