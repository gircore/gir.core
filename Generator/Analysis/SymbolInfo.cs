using System;
using Generator.Introspection;

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
    public interface ISymbolInfo
    {
        // Fixed Information
        public SymbolType Type { get; }
        public QualifiedName NativeName { get; }
        public Classification Classification { get; }
        
        // Transformable Information
        public QualifiedName ManagedName { get; }
    }

    public record ObjectSymbol : ISymbolInfo
    {
        // Fixed Information
        public SymbolType Type => SymbolType.Object;
        public Classification Classification => Classification.Reference;
        public QualifiedName NativeName { get; }
        public GClass ClassInfo { get; }
        
        // Transformable Information
        public QualifiedName ManagedName { get; }

        public ObjectSymbol(QualifiedName nativeName, QualifiedName managedName, GClass classInfo)
        {
            NativeName = nativeName;
            ManagedName = managedName;
            ClassInfo = classInfo;
        }
    }
    
    public record InterfaceSymbol : ISymbolInfo
    {
        // Fixed Information
        public SymbolType Type => SymbolType.Interface;
        public Classification Classification => Classification.Reference;
        public QualifiedName NativeName { get; }
        public GInterface InterfaceInfo { get; }
        
        // Transformable Information
        public QualifiedName ManagedName { get; }
        
        public InterfaceSymbol(QualifiedName nativeName, QualifiedName managedName, GInterface interfaceInfo)
        {
            NativeName = nativeName;
            ManagedName = managedName;
            InterfaceInfo = interfaceInfo;
        }
    }
    
    
}
