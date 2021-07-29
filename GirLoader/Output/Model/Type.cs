using System.Collections.Generic;

namespace GirLoader.Output.Model
{
    public abstract class Type: TypeReferenceProvider, Resolveable
    {
        public Repository? Repository { get; }
        public Metadata Metadata { get; } = new();
        
        public TypeName Name { get; set; }

        /// <summary>
        /// Name of the symbol in the c world
        /// </summary>
        public CType? CType { get; }

        protected internal Type(Repository? repository, CType? cType, TypeName name) 
        {
            Repository = repository;
            CType = cType;
            Name = name;
        }

        internal virtual void Strip() { }

        public string GetMetadataString(string key)
            => Metadata[key]?.ToString() ?? "";

        internal abstract bool Matches(TypeReference typeReference);
        public abstract IEnumerable<TypeReference> GetTypeReferences();
        public abstract bool GetIsResolved();
    }
}
