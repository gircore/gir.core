using System.Collections.Generic;

namespace GirLoader.Output
{
    public abstract class Type : GirModel.Type
    {
        public Metadata Metadata { get; } = new();

        /// <summary>
        /// Name of the symbol in the c world
        /// </summary>
        public CType? CType { get; }

        protected internal Type(CType? cType)
        {
            CType = cType;
        }

        internal virtual void Strip() { }

        public string GetMetadataString(string key)
            => Metadata[key]?.ToString() ?? "";

        internal abstract bool Matches(TypeReference typeReference);
        internal abstract IEnumerable<TypeReference> GetTypeReferences();
        internal abstract bool GetIsResolved();
    }
}
