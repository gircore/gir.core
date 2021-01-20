using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

#nullable enable

namespace Generator.Analysis
{
    // Maps Symbol Name -> Introspection Data
    // Namespace-specific
    internal class SymbolDictionary
    {
        private readonly Dictionary<string, ISymbolInfo> _symbolDict = new();
        internal string Namespace { get; }

        public SymbolDictionary(string nspace)
        {
            Namespace = nspace;
        }

        public void AddSymbol(string name, ISymbolInfo info)
            => _symbolDict.Add(name, info);

        public ISymbolInfo GetSymbol(string name)
            => _symbolDict[name];

        public bool TryGetSymbol(string name, [NotNullWhen(true)] out ISymbolInfo symbol)
            => _symbolDict.TryGetValue(name, out symbol);
    }

    // Maps Alias -> Symbol Name
    // Namespace-specific
    internal class AliasDictionary
    {
        private readonly Dictionary<string, string> _aliasDict = new();
        internal string Namespace { get; }

        public AliasDictionary(string nspace)
        {
            Namespace = nspace;
        }
        
        public void AddAlias(string from, string to)
            => _aliasDict.Add(from, to);
        
        public string GetAlias(string name)
            => _aliasDict[name];
        
        public bool TryGetAlias(string name, [NotNullWhen(true)] out string alias)
            => _aliasDict.TryGetValue(name, out alias);
    }
    
    public class TypeDictionary
    {
        private readonly Dictionary<string, SymbolDictionary> _symbolDictionaries = new();
        private readonly Dictionary<string, AliasDictionary> _aliasDictionaries = new();

        private readonly SymbolDictionary _defaultDict = new(null);
        
        // private readonly Dictionary<QualifiedName, ISymbolInfo> _typeDict = new();
        // private readonly Dictionary<QualifiedName, QualifiedName> _aliasDict = new();

        public TypeDictionary()
        {
            // Add Fundamental Types
            // Fundamental types are accessible regardless of namespace and
            // take priority over any namespaced variant.
            
            AddBasicSymbol(new BasicSymbol("none", "void"));
            AddBasicSymbol(new BasicSymbol("any", "IntPtr"));
            
            AddBasicSymbol(new BasicSymbol("void", "void"));
            AddBasicSymbol(new BasicSymbol("gboolean", "bool"));
            AddBasicSymbol(new BasicSymbol("gfloat", "float"));
            AddBasicSymbol(new BasicSymbol("float", "float"));
            
            AddBasicSymbol(new BasicSymbol("gconstpointer", "IntPtr"));
            AddBasicSymbol(new BasicSymbol("va_list", "IntPtr"));
            AddBasicSymbol(new BasicSymbol("gpointer", "IntPtr"));
            AddBasicSymbol(new BasicSymbol("GType", "IntPtr"));
            AddBasicSymbol(new BasicSymbol("tm", "IntPtr"));
            
            AddBasicSymbol(new BasicSymbol("guint16", "ushort"));
            AddBasicSymbol(new BasicSymbol("gushort", "ushort"));
            
            AddBasicSymbol(new BasicSymbol("gint16", "short"));
            AddBasicSymbol(new BasicSymbol("gshort", "short"));
            
            AddBasicSymbol(new BasicSymbol("double", "double"));
            AddBasicSymbol(new BasicSymbol("gdouble", "double"));
            AddBasicSymbol(new BasicSymbol("long double", "double"));
            
            // AddBasicSymbol(new BasicSymbol("cairo_format_t", "int"));
            AddBasicSymbol(new BasicSymbol("int", "int"));
            AddBasicSymbol(new BasicSymbol("gint", "int"));
            AddBasicSymbol(new BasicSymbol("gint32", "int"));
            AddBasicSymbol(new BasicSymbol("pid_t", "int"));
            
            AddBasicSymbol(new BasicSymbol("unsigned int", "uint"));
            AddBasicSymbol(new BasicSymbol("unsigned", "uint"));
            AddBasicSymbol(new BasicSymbol("guint", "uint"));
            AddBasicSymbol(new BasicSymbol("guint32", "uint"));
            AddBasicSymbol(new BasicSymbol("gunichar", "uint"));
            AddBasicSymbol(new BasicSymbol("uid_t", "uint"));
            // AddBasicSymbol(new BasicSymbol("GQuark", "uint"));
            
            AddBasicSymbol(new BasicSymbol("guchar", "byte"));
            AddBasicSymbol(new BasicSymbol("gchar", "byte"));
            AddBasicSymbol(new BasicSymbol("char", "byte"));
            AddBasicSymbol(new BasicSymbol("guint8", "byte"));
            AddBasicSymbol(new BasicSymbol("gint8", "byte"));
            
            AddBasicSymbol(new BasicSymbol("glong", "long"));
            AddBasicSymbol(new BasicSymbol("gssize", "long"));
            AddBasicSymbol(new BasicSymbol("gint64", "long"));
            AddBasicSymbol(new BasicSymbol("goffset", "long"));
            AddBasicSymbol(new BasicSymbol("time_t", "long"));
            
            AddBasicSymbol(new BasicSymbol("gsize", "ulong"));
            AddBasicSymbol(new BasicSymbol("guint64", "ulong"));
            AddBasicSymbol(new BasicSymbol("gulong", "ulong"));
            
            AddBasicSymbol(new BasicSymbol("utf8", "string"));
            // AddBasicSymbol(new BasicSymbol("Window", "ulong"));
        }
        
        /// <summary>
        /// Adds a symbol under the given namespace.
        /// </summary>
        /// <param name="nspace"></param>
        /// <param name="info"></param>
        public void AddSymbol(string nspace, ISymbolInfo info)
        {
            GetSymbolDict(nspace).AddSymbol(info.NativeName.Type, info);
        }

        /// <summary>
        /// Adds a string alias under the given namespace.
        /// </summary>
        /// <param name="nspace"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        public void AddAlias(string nspace, string from, string to)
        {
            GetAliasDict(nspace).AddAlias(from, to);
        }

        public TypeDictionaryView GetView(string nspace)
            => new TypeDictionaryView(this, nspace);

        private SymbolDictionary GetSymbolDict(string nspace)
        {
            if (nspace == null)
                throw new NullReferenceException("Namespace should not be null");

            if (_symbolDictionaries.TryGetValue(nspace, out SymbolDictionary? symbolDict))
                return symbolDict;

            var dict = new SymbolDictionary(nspace);
            _symbolDictionaries.Add(nspace, dict);
            return dict;
        }
        
        private AliasDictionary GetAliasDict(string nspace)
        {
            if (nspace == null)
                throw new NullReferenceException("Namespace should not be null");
            
            if (_aliasDictionaries.TryGetValue(nspace, out AliasDictionary? aliasDict))
                return aliasDict;

            var dict = new AliasDictionary(nspace);
            _aliasDictionaries.Add(nspace, dict);
            return dict;
        }
        
        private void AddBasicSymbol(BasicSymbol basicSymbol)
        {
            _defaultDict.AddSymbol(basicSymbol.NativeName.Type, basicSymbol);
        }
        
        private ISymbolInfo GetSymbolInternal(SymbolDictionary symbolDict, string symbol)
        {
            // Check Fundamental Types
            if (_defaultDict.TryGetSymbol(symbol, out ISymbolInfo info))
                return info;

            // Check Normal
            return symbolDict.GetSymbol(symbol);
        }
        
        /// <summary>
        /// Queries the type dictionary for the given type under the provided namespace. If
        /// <see cref="symbol"/> refers to the name of a <b>fundamental type</b>, the
        /// default dictionary will take priority over any namespace. It is an error
        /// for a namespaced type to share the same name as a fundamental type.
        /// </summary>
        /// <param name="nspace">Namespace to search</param>
        /// <param name="symbol">Unqualified name of symbol (i.e. does not contain '.')</param>
        /// <returns>Information about the symbol</returns>
        public ISymbolInfo GetSymbol(string? nspace, string symbol)
        {
            Debug.Assert(
                condition: !symbol.Contains('.'),
                message: $"Symbol {symbol} should not be qualified by a namespace"
            );
            
            if (nspace == null)
                return _defaultDict.GetSymbol(symbol);
            
            // Get Namespace-specific Dictionaries
            SymbolDictionary symbolDict = GetSymbolDict(nspace);
            AliasDictionary aliasDict = GetAliasDict(nspace);
            
            // Check Aliases
            if (aliasDict.TryGetAlias(symbol, out var alias))
                return GetSymbolInternal(symbolDict, alias);

            // Check Normal
            return GetSymbolInternal(symbolDict, symbol);
        }
    }

    // A view of the type dictionary from a specific namespace (Immutable access only)
    public class TypeDictionaryView
    {
        public TypeDictionary TypeDict { get; }
        public string Namespace { get; }
        
        public TypeDictionaryView(TypeDictionary typeDict, string nspace)
        {
            TypeDict = typeDict;
            Namespace = nspace;
        }

        /// <summary>
        /// Looks up <see cref="symbolName"/> from the perspective of the
        /// <see cref="TypeDictionaryView"/>'s namespace. Unqualified types
        /// are looked up in the current namespace, while Qualified types
        /// are searched globally. Fundamental types take priority over any
        /// other type name.
        /// </summary>
        /// <param name="symbolName">Name of the symbol. May be qualified or unqualified</param>
        /// <returns>Information about the symbol</returns>
        public ISymbolInfo LookupSymbol(string symbolName)
        {
            if (symbolName.Contains('.'))
            {
                // We are in the form 'Namespace.Type'
                var components = symbolName.Split('.', 2);
                return TypeDict.GetSymbol(components[0], components[1]);
            }

            // We are not qualified by a namespace, so assume this one.
            // It might also be a fundamental type, but the type dict
            // takes care of this.
            return TypeDict.GetSymbol(Namespace, symbolName);
        }
    }
}
