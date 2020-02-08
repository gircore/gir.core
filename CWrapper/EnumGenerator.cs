namespace CWrapper
{
    internal partial class EnumGenerator
    {
        private Namespace Namespace;
        private Enum Enum;

        public EnumGenerator(Namespace ns, Enum enumeration)
        {
            Namespace = ns ?? throw new System.ArgumentNullException(nameof(ns));
            Enum = enumeration ?? throw new System.ArgumentNullException(nameof(enumeration));
        }
    }
}