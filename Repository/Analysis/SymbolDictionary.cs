using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using Repository.Model;

namespace Repository.Analysis
{
    internal partial class SymbolDictionary
    {
        private readonly Dictionary<string, TypeDictionary> _symbolDictionaries = new();
        private readonly Dictionary<string, TypeDictionary> _aliasDictionaries = new();

        private readonly TypeDictionary _defaultDict = new();

        public SymbolDictionary()
        {
            // Add Fundamental Types
            // Fundamental types are accessible regardless of namespace and
            // take priority over any namespaced variant.

            AddDefaultSymbol(Symbol.Primitive("none", "void"));
            AddDefaultSymbol(Symbol.Primitive("any", "IntPtr"));

            AddDefaultSymbol(Symbol.Primitive("void", "void"));
            AddDefaultSymbol(Symbol.Primitive("gboolean", "bool"));
            AddDefaultSymbol(Symbol.Primitive("gfloat", "float"));
            AddDefaultSymbol(Symbol.Primitive("float", "float"));

            AddDefaultSymbol(Symbol.Primitive("gconstpointer", "IntPtr"));
            AddDefaultSymbol(Symbol.Primitive("va_list", "IntPtr"));
            AddDefaultSymbol(Symbol.Primitive("gpointer", "IntPtr"));
            AddDefaultSymbol(Symbol.Primitive("GType", "IntPtr"));
            AddDefaultSymbol(Symbol.Primitive("tm", "IntPtr"));
            
            // TODO: Should we use UIntPtr here? Non-CLR compliant
            AddDefaultSymbol(Symbol.Primitive("guintptr", "UIntPtr"));

            AddDefaultSymbol(Symbol.Primitive("guint16", "ushort"));
            AddDefaultSymbol(Symbol.Primitive("gushort", "ushort"));
            AddDefaultSymbol(Symbol.Primitive("gunichar2", "ushort"));
            
            AddDefaultSymbol(Symbol.Primitive("gint16", "short"));
            AddDefaultSymbol(Symbol.Primitive("gshort", "short"));

            AddDefaultSymbol(Symbol.Primitive("double", "double"));
            AddDefaultSymbol(Symbol.Primitive("gdouble", "double"));
            AddDefaultSymbol(Symbol.Primitive("long double", "double"));

            // AddBasicSymbol(new BasicSymbol("cairo_format_t", "int"));
            AddDefaultSymbol(Symbol.Primitive("int", "int"));
            AddDefaultSymbol(Symbol.Primitive("gint", "int"));
            AddDefaultSymbol(Symbol.Primitive("gint32", "int"));
            AddDefaultSymbol(Symbol.Primitive("pid_t", "int"));

            AddDefaultSymbol(Symbol.Primitive("unsigned int", "uint"));
            AddDefaultSymbol(Symbol.Primitive("unsigned", "uint"));
            AddDefaultSymbol(Symbol.Primitive("guint", "uint"));
            AddDefaultSymbol(Symbol.Primitive("guint32", "uint"));
            AddDefaultSymbol(Symbol.Primitive("gunichar", "uint"));
            AddDefaultSymbol(Symbol.Primitive("uid_t", "uint"));
            // AddBasicSymbol(new BasicSymbol("GQuark", "uint"));

            AddDefaultSymbol(Symbol.Primitive("guchar", "byte"));
            AddDefaultSymbol(Symbol.Primitive("gchar", "sbyte"));
            AddDefaultSymbol(Symbol.Primitive("char", "sbyte"));
            AddDefaultSymbol(Symbol.Primitive("guint8", "byte"));
            AddDefaultSymbol(Symbol.Primitive("gint8", "sbyte"));

            AddDefaultSymbol(Symbol.Primitive("glong", "long"));
            AddDefaultSymbol(Symbol.Primitive("gssize", "long"));
            AddDefaultSymbol(Symbol.Primitive("gint64", "long"));
            AddDefaultSymbol(Symbol.Primitive("goffset", "long"));
            AddDefaultSymbol(Symbol.Primitive("time_t", "long"));

            AddDefaultSymbol(Symbol.Primitive("gsize", "ulong"));
            AddDefaultSymbol(Symbol.Primitive("guint64", "ulong"));
            AddDefaultSymbol(Symbol.Primitive("gulong", "ulong"));

            AddDefaultSymbol(Symbol.Primitive("utf8", "string"));
            AddDefaultSymbol(Symbol.Primitive("filename", "string"));
            // AddBasicSymbol(new BasicSymbol("Window", "ulong"));
        }

        /// <summary>
        /// Adds a symbol under the given namespace.
        /// </summary>
        /// <param name="nspace"></param>
        /// <param name="info"></param>
        public void AddSymbol(string nspace, Symbol info)
        {
            GetTypeDict(nspace).AddAlias(info);
        }       

        public void AddSymbols(string nspace, IEnumerable<Symbol> infos)
        {
            foreach (Symbol info in infos)
                AddSymbol(nspace, info);
        }
        
        public void AddAlias(string nspace, Symbol symbol)
        {
            GetAliasDict(nspace).AddAlias(symbol);
        }

        public SymbolDictionaryView GetView(string nspace)
            => new SymbolDictionaryView(this, nspace);

        private TypeDictionary GetTypeDict(string nspace)
        {
            if (_symbolDictionaries.TryGetValue(nspace, out TypeDictionary? symbolDict))
                return symbolDict;

            var dict = new TypeDictionary();
            _symbolDictionaries.Add(nspace, dict);
            return dict;
        }

        private TypeDictionary GetAliasDict(string nspace)
        {
            if (_aliasDictionaries.TryGetValue(nspace, out TypeDictionary? aliasDict))
                return aliasDict;

            var dict = new TypeDictionary();
            _aliasDictionaries.Add(nspace, dict);
            return dict;
        }

        private void AddDefaultSymbol(Symbol symbol)
        {
            _defaultDict.AddAlias(symbol);
        }

        private bool GetSymbolInternal(TypeDictionary symbolDict, string symbol, [MaybeNullWhen(false)] out Symbol found)
        {
            // Check Fundamental Types
            if (_defaultDict.TryGetAlias(symbol, out found))
                return true;

            // Check Normal
            return symbolDict.TryGetAlias(symbol, out found);
        }

        /// <summary>
        /// Queries the type dictionary for the given type under the provided namespace. If
        /// <see cref="symbol"/> refers to the name of a <b>fundamental type</b>, the
        /// default dictionary will take priority over any namespace. It is an error
        /// for a namespaced type to share the same name as a fundamental type.
        /// </summary>
        /// <param name="nspace">Namespace to search</param>
        /// <param name="symbol">Unqualified name of symbol (i.e. does not contain '.')</param>
        /// <param name="found"></param>
        /// <returns>Information about the symbol</returns>
        public bool GetSymbol(string? nspace, string? symbol, string? ctype, [MaybeNullWhen(false)] out Symbol found)
        {
            Debug.Assert(
                condition: !symbol?.Contains('.') ?? true,
                message: $"Symbol {symbol} should not be qualified by a namespace"
            );

            if (nspace is null)
            {
                if(symbol is not null)
                    return _defaultDict.TryGetAlias(symbol, ctype, out found);

                throw new Exception("Can not get symbol because namespace is not given but symbol is empty");
            }
               

            // Get Namespace-specific Dictionaries
            TypeDictionary symbolDict = GetTypeDict(nspace);
            TypeDictionary aliasDict = GetAliasDict(nspace);

            // Check Aliases (TODO: Should this use type references?)
            if (aliasDict.TryGetAlias(symbol, ctype, out var alias))
            {
                if (alias.Contains('.'))
                {
                    // Reference to other namespace
                    var components = alias.Split('.', 2);
                    return GetSymbol(nspace: components[0], symbol: components[1], ctype, out found);
                }

                // Within this namespace
                return GetSymbolInternal(symbolDict, alias, out found);
            }

            // Check Normal
            return GetSymbolInternal(symbolDict, symbol, out found);
        }
    }
}
