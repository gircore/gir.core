using System.Collections.Generic;

namespace GirLoader.Output
{
    public abstract class Type : GirModel.Type
    {
        /// <summary>
        /// Name of the symbol in the c world
        /// </summary>
        public string? CType { get; }

        protected internal Type(string? cType)
        {
            CType = cType;
        }

        internal virtual void Strip() { }

        internal abstract bool Matches(TypeReference typeReference);
        internal abstract IEnumerable<TypeReference> GetTypeReferences();
        internal abstract bool GetIsResolved();
    }
}
