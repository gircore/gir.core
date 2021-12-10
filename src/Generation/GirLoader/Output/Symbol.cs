using System.Collections.Generic;

namespace GirLoader.Output
{
    public abstract class Symbol
    {
        public string OriginalName { get; }

        protected Symbol(string originalName)
        {
            OriginalName = originalName;
        }

        internal abstract IEnumerable<TypeReference> GetTypeReferences();
        internal abstract bool GetIsResolved();
    }
}
