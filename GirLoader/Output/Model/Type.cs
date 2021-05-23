namespace GirLoader.Output.Model
{
    public abstract class Type : Symbol
    {
        public Repository? Repository { get; }
        public Metadata Metadata { get; } = new();

        /// <summary>
        /// Name of the symbol in the c world
        /// </summary>
        public CTypeName? CTypeName { get; }

        protected internal Type(CTypeName ctypeName, SymbolName name) : this(ctypeName, name, name)
        {
        }

        protected internal Type(CTypeName? ctypeName, SymbolName originalName, SymbolName symbolName) : this(null, ctypeName, originalName, symbolName)
        {
        }

        protected internal Type(Repository? repository, CTypeName? cTypeName, SymbolName orignalName, SymbolName symbolName) : base(orignalName, symbolName)
        {
            Repository = repository;
            CTypeName = cTypeName;
        }

        internal virtual void Strip() { }

        public string GetMetadataString(string key)
            => Metadata[key]?.ToString() ?? "";
    }
}
