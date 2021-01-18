namespace Generator.Analysis
{
    public enum Classification
    {
        Value,
        Reference
    }

    public record QualifiedName
    {
        public string nspace;
        public string type;

        public QualifiedName(string nspace, string type)
        {
            this.nspace = nspace;
            this.type = type;
        }
    }

    public enum SymbolType
    {
        Object,
        Interface,
        Record,
        Delegate,
        Enumeration,
        Constant,
        Method
    }
    
    // Replace with ISymbolInfo interface and
    // concrete types for each symbol.
    // For example: InterfaceSymbol, ObjectSymbol, etc
    public record SymbolInfo
    {
        // Fixed Information
        public readonly SymbolType type;
        public readonly QualifiedName nativeName;
        public readonly Classification classification;
        
        // Transformable Information
        public QualifiedName managedName;

        public SymbolInfo(SymbolType type,
            QualifiedName nativeName,
            QualifiedName managedName,
            Classification classification)
        {
            this.type = type;
            this.nativeName = nativeName;
            this.managedName = managedName;
            this.classification = classification;
        }
    }
}
