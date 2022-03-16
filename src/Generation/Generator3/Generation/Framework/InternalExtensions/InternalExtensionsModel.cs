using Generator3.Converter;

namespace Generator3.Generation.Framework
{
    public class InternalExtensionsModel
    {
        private readonly GirModel.Namespace _ns;

        public string NamespaceName => _ns.GetInternalName();

        public InternalExtensionsModel(GirModel.Namespace ns)
        {
            _ns = ns;
        }
    }
}
