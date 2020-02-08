namespace CWrapper
{
    internal partial class StructGenerator
    {
        private Namespace Namespace;
        private readonly Struct Struct;

        public StructGenerator(Namespace ns, Struct @struct)
        {
            Namespace = ns ?? throw new System.ArgumentNullException(nameof(ns));
            Struct = @struct ?? throw new System.ArgumentNullException(nameof(@struct));
        }
    }
}