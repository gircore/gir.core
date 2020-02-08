using System.Collections.Generic;

namespace CWrapper
{
    internal partial class DelegatesGenerator
    {
        private Namespace Namespace;
        private readonly IEnumerable<Delegate> Delegates;

        public DelegatesGenerator(Namespace ns, IEnumerable<Delegate> delegates)
        {
            Namespace = ns ?? throw new System.ArgumentNullException(nameof(ns));
            Delegates = delegates ?? throw new System.ArgumentNullException(nameof(delegates));
        }
    }
}