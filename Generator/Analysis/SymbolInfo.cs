using System;
using System.Diagnostics;

namespace Repository.Xml.Analysis
{
    public enum Classification
    {
        Value,
        Reference,
        Closure // TODO: Should this be a classification?
    }

    public record QualifiedName
    {
        public string Namespace { get; set; }
        public string Type { get; set; }

        public QualifiedName(string nspace, string type)
        {
            Namespace = nspace;
            Type = type;
        }

        public override string ToString()
        {
            if (!string.IsNullOrEmpty(Namespace))
                return $"{Namespace}.{Type}";

            return Type;
        }
    }

    public enum SymbolType
    {
        Basic,
        Object,
        Interface,
        Record,
        Delegate,
        Enumeration,
        Constant,
        Method
    }
    
    // TODO: Add CType property?
    public interface ISymbolInfo
    {
        // Fixed Information
        public SymbolType Type { get; }
        public QualifiedName NativeName { get; }
        public Classification Classification { get; }
        
        // Transformable Information
        public QualifiedName ManagedName { get; }
    }

    // For C# Built-in/Keyword Types
    public record BasicSymbol : ISymbolInfo
    {
        // Fixed Information
        public SymbolType Type => SymbolType.Basic;
        public QualifiedName NativeName { get; }
        public Classification Classification => Classification.Value;
        
        // Transformable Information
        public QualifiedName ManagedName { get; }

        public BasicSymbol(string nativeType, string managedType)
        {
            Debug.Assert(!nativeType.Contains('.'), "Basic types names cannot be qualified");
            NativeName = new QualifiedName(string.Empty, nativeType);
            ManagedName = new QualifiedName(string.Empty, managedType);
        }
    }

    public record ObjectSymbol : ISymbolInfo
    {
        // Fixed Information
        public SymbolType Type => SymbolType.Object;
        public Classification Classification => Classification.Reference;
        public QualifiedName NativeName { get; }
        
        // Transformable Information
        public QualifiedName ManagedName { get; }

        public ObjectSymbol(QualifiedName nativeName, QualifiedName managedName)
        {
            NativeName = nativeName;
            ManagedName = managedName;
        }
    }
    
    public record EnumSymbol : ISymbolInfo
    {
        // Fixed Information
        public SymbolType Type => SymbolType.Enumeration;
        public Classification Classification => Classification.Value;
        public QualifiedName NativeName { get; }
        public bool Flags { get;  }
        
        // Transformable Information
        public QualifiedName ManagedName { get; }

        public EnumSymbol(QualifiedName nativeName, QualifiedName managedName, bool flags = false)
        {
            NativeName = nativeName;
            ManagedName = managedName;
            Flags = flags;
        }
    }
    
    public record RecordSymbol : ISymbolInfo
    {
        // Fixed Information
        public SymbolType Type => SymbolType.Record;
        public Classification Classification => Classification.Value;
        public QualifiedName NativeName { get; }
        
        // Transformable Information
        public QualifiedName ManagedName { get; }

        public RecordSymbol(QualifiedName nativeName, QualifiedName managedName)
        {
            NativeName = nativeName;
            ManagedName = managedName;
        }
    }

    public record DelegateSymbol : ISymbolInfo
    {
        // Fixed Information
        public SymbolType Type => SymbolType.Delegate;
        public Classification Classification => Classification.Closure;
        public QualifiedName NativeName { get; }
        
        // Transformable Information
        public QualifiedName ManagedName { get; }

        public DelegateSymbol(QualifiedName nativeName, QualifiedName managedName)
        {
            NativeName = nativeName;
            ManagedName = managedName;
        }
    }
    
    public record InterfaceSymbol : ISymbolInfo
    {
        // Fixed Information
        public SymbolType Type => SymbolType.Interface;
        public Classification Classification => Classification.Reference;
        public QualifiedName NativeName { get; }
        
        // Transformable Information
        public QualifiedName ManagedName { get; }
        
        public InterfaceSymbol(QualifiedName nativeName, QualifiedName managedName)
        {
            NativeName = nativeName;
            ManagedName = managedName;
        }
    }
}
