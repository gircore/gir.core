using Generator3.Converter;

namespace Generator3.Generation.Framework
{
    public class ModuleDllImportModel
    {
        private readonly GirModel.Namespace _ns;

        public string NamespaceName => _ns.GetPublicName();

        public ModuleDllImportModel(GirModel.Namespace ns)
        {
            _ns = ns;
        }
    }
}
