using System.Collections.Generic;
using System.Diagnostics;
using Repository.Model;

namespace Repository.Analysis
{
    internal partial class SymbolDictionary
    {
        private readonly Dictionary<string, TypeDictionary> _symbolDictionaries = new();
        private readonly Dictionary<string, AliasDictionary> _aliasDictionaries = new();

        private readonly TypeDictionary _defaultDict = new(null);

        public SymbolDictionary()
        {
            // Add Fundamental Types
            // Fundamental types are accessible regardless of namespace and
            // take priority over any namespaced variant.

            AddSymbol(new Symbol("none", "void"));
            AddSymbol(new Symbol("any", "IntPtr"));

            AddSymbol(new Symbol("void", "void"));
            AddSymbol(new Symbol("gboolean", "bool"));
            AddSymbol(new Symbol("gfloat", "float"));
            AddSymbol(new Symbol("float", "float"));

            AddSymbol(new Symbol("gconstpointer", "IntPtr"));
            AddSymbol(new Symbol("va_list", "IntPtr"));
            AddSymbol(new Symbol("gpointer", "IntPtr"));
            AddSymbol(new Symbol("GType", "IntPtr"));
            AddSymbol(new Symbol("tm", "IntPtr"));
            
            // TODO: Should we use UIntPtr here? Non-CLR compliant
            AddSymbol(new Symbol("guintptr", "UIntPtr"));

            AddSymbol(new Symbol("guint16", "ushort"));
            AddSymbol(new Symbol("gushort", "ushort"));

            AddSymbol(new Symbol("gint16", "short"));
            AddSymbol(new Symbol("gshort", "short"));

            AddSymbol(new Symbol("double", "double"));
            AddSymbol(new Symbol("gdouble", "double"));
            AddSymbol(new Symbol("long double", "double"));

            // AddBasicSymbol(new BasicSymbol("cairo_format_t", "int"));
            AddSymbol(new Symbol("int", "int"));
            AddSymbol(new Symbol("gint", "int"));
            AddSymbol(new Symbol("gint32", "int"));
            AddSymbol(new Symbol("pid_t", "int"));

            AddSymbol(new Symbol("unsigned int", "uint"));
            AddSymbol(new Symbol("unsigned", "uint"));
            AddSymbol(new Symbol("guint", "uint"));
            AddSymbol(new Symbol("guint32", "uint"));
            AddSymbol(new Symbol("gunichar", "uint"));
            AddSymbol(new Symbol("uid_t", "uint"));
            // AddBasicSymbol(new BasicSymbol("GQuark", "uint"));

            AddSymbol(new Symbol("guchar", "byte"));
            AddSymbol(new Symbol("gchar", "byte"));
            AddSymbol(new Symbol("char", "byte"));
            AddSymbol(new Symbol("guint8", "byte"));
            AddSymbol(new Symbol("gint8", "byte"));

            AddSymbol(new Symbol("glong", "long"));
            AddSymbol(new Symbol("gssize", "long"));
            AddSymbol(new Symbol("gint64", "long"));
            AddSymbol(new Symbol("goffset", "long"));
            AddSymbol(new Symbol("time_t", "long"));

            AddSymbol(new Symbol("gsize", "ulong"));
            AddSymbol(new Symbol("guint64", "ulong"));
            AddSymbol(new Symbol("gulong", "ulong"));

            AddSymbol(new Symbol("utf8", "string"));
            AddSymbol(new Symbol("filename", "string"));
            // AddBasicSymbol(new BasicSymbol("Window", "ulong"));
        }

        /// <summary>
        /// Adds a symbol under the given namespace.
        /// </summary>
        /// <param name="nspace"></param>
        /// <param name="info"></param>
        public void AddSymbol(string nspace, Symbol info)
        {
            GetTypeDict(nspace).AddSymbol(info.Name, info);
        }

        public void AddSymbols(string nspace, IEnumerable<Symbol> infos)
        {
            TypeDictionary dict = GetTypeDict(nspace);
            foreach (Symbol info in infos)
                AddSymbol(nspace, info);
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

        public SymbolDictionaryView GetView(string nspace)
            => new SymbolDictionaryView(this, nspace);

        private TypeDictionary GetTypeDict(string nspace)
        {
            if (_symbolDictionaries.TryGetValue(nspace, out TypeDictionary? symbolDict))
                return symbolDict;

            var dict = new TypeDictionary(nspace);
            _symbolDictionaries.Add(nspace, dict);
            return dict;
        }

        private AliasDictionary GetAliasDict(string nspace)
        {
            if (_aliasDictionaries.TryGetValue(nspace, out AliasDictionary? aliasDict))
                return aliasDict;

            var dict = new AliasDictionary(nspace);
            _aliasDictionaries.Add(nspace, dict);
            return dict;
        }

        private void AddSymbol(Symbol symbol)
        {
            _defaultDict.AddSymbol(symbol.Name, symbol);
        }

        private Symbol GetSymbolInternal(TypeDictionary symbolDict, string symbol)
        {
            // Check Fundamental Types
            if (_defaultDict.TryGetSymbol(symbol, out Symbol? info))
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
        public Symbol GetSymbol(string? nspace, string symbol)
        {
            Debug.Assert(
                condition: !symbol.Contains('.'),
                message: $"Symbol {symbol} should not be qualified by a namespace"
            );

            if (nspace == null)
                return _defaultDict.GetSymbol(symbol);

            // Get Namespace-specific Dictionaries
            TypeDictionary symbolDict = GetTypeDict(nspace);
            AliasDictionary aliasDict = GetAliasDict(nspace);

            // Check Aliases (TODO: Should this use type references?)
            if (aliasDict.TryGetAlias(symbol, out var alias))
            {
                if (alias.Contains('.'))
                {
                    // Reference to other namespace
                    var components = alias.Split('.', 2);
                    return GetSymbol(nspace: components[0], symbol: components[1]);
                }

                // Within this namespace
                return GetSymbolInternal(symbolDict, alias);
            }

            // Check Normal
            return GetSymbolInternal(symbolDict, symbol);
        }
    }
}
