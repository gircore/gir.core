namespace Generator3.Generation.Framework
{
    public class InternalExtensionsModel
    {
        private readonly string _namespace;

        public string NamespaceName => _namespace + ".Internal";

        public InternalExtensionsModel(string @namespace)
        {
            this._namespace = @namespace;
        }
    }
}
