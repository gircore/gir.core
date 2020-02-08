namespace CWrapper
{
    internal partial class ClassGenerator
    {
        private Namespace Namespace;
        private Class Class;

        public ClassGenerator(Namespace ns, Class cls)
        {
            Namespace = ns ?? throw new System.ArgumentNullException(nameof(ns));
            Class = cls ?? throw new System.ArgumentNullException(nameof(cls));
        }
    }
}