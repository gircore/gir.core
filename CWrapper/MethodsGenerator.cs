using System.Collections.Generic;

namespace CWrapper
{
    internal partial class MethodsGenerator
    {
        private Namespace Namespace;
        private IEnumerable<Method> Methods;

        public MethodsGenerator(Namespace ns, IEnumerable<Method> methods)
        {
            Namespace = ns ?? throw new System.ArgumentNullException(nameof(ns));
            Methods = methods ?? throw new System.ArgumentNullException(nameof(methods));
        }
    }
}