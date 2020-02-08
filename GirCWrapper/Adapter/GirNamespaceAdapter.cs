using CWrapper;

namespace Gir
{
    public class GirNamespaceAdapter : Namespace
    {
        private readonly GNamespace ns;

        public string Name => ns.Name;

        public GirNamespaceAdapter(GNamespace ns)
        {
            this.ns = ns ?? throw new System.ArgumentNullException(nameof(ns));
        }
    }
}