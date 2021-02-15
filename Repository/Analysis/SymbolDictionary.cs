using System.Collections.Generic;
using System.Diagnostics;
using Repository.Model;

namespace Repository.Analysis
{
    public partial class SymbolDictionary
    {
        private readonly Dictionary<string, TypeDictionary> _symbolDictionaries = new();
        private readonly Dictionary<string, AliasDictionary> _aliasDictionaries = new();

        private readonly TypeDictionary _defaultDict = new(null);

        public SymbolDictionary()
        {
            // Add Fundamental Types
            // Fundamental types are accessible regardless of namespace and
            // take priority over any namespaced variant.

            AddSymbol(new BasicSymbol("none", "void"));
            AddSymbol(new BasicSymbol("any", "IntPtr"));

            AddSymbol(new BasicSymbol("void", "void"));
            AddSymbol(new BasicSymbol("gboolean", "bool"));
            AddSymbol(new BasicSymbol("gfloat", "float"));
            AddSymbol(new BasicSymbol("float", "float"));

            AddSymbol(new BasicSymbol("gconstpointer", "IntPtr"));
            AddSymbol(new BasicSymbol("va_list", "IntPtr"));
            AddSymbol(new BasicSymbol("gpointer", "IntPtr"));
            AddSymbol(new BasicSymbol("GType", "IntPtr"));
            AddSymbol(new BasicSymbol("tm", "IntPtr"));

            AddSymbol(new BasicSymbol("guint16", "ushort"));
            AddSymbol(new BasicSymbol("gushort", "ushort"));

            AddSymbol(new BasicSymbol("gint16", "short"));
            AddSymbol(new BasicSymbol("gshort", "short"));

            AddSymbol(new BasicSymbol("double", "double"));
            AddSymbol(new BasicSymbol("gdouble", "double"));
            AddSymbol(new BasicSymbol("long double", "double"));

            // AddBasicSymbol(new BasicSymbol("cairo_format_t", "int"));
            AddSymbol(new BasicSymbol("int", "int"));
            AddSymbol(new BasicSymbol("gint", "int"));
            AddSymbol(new BasicSymbol("gint32", "int"));
            AddSymbol(new BasicSymbol("pid_t", "int"));

            AddSymbol(new BasicSymbol("unsigned int", "uint"));
            AddSymbol(new BasicSymbol("unsigned", "uint"));
            AddSymbol(new BasicSymbol("guint", "uint"));
            AddSymbol(new BasicSymbol("guint32", "uint"));
            AddSymbol(new BasicSymbol("gunichar", "uint"));
            AddSymbol(new BasicSymbol("uid_t", "uint"));
            // AddBasicSymbol(new BasicSymbol("GQuark", "uint"));

            AddSymbol(new BasicSymbol("guchar", "byte"));
            AddSymbol(new BasicSymbol("gchar", "byte"));
            AddSymbol(new BasicSymbol("char", "byte"));
            AddSymbol(new BasicSymbol("guint8", "byte"));
            AddSymbol(new BasicSymbol("gint8", "byte"));

            AddSymbol(new BasicSymbol("glong", "long"));
            AddSymbol(new BasicSymbol("gssize", "long"));
            AddSymbol(new BasicSymbol("gint64", "long"));
            AddSymbol(new BasicSymbol("goffset", "long"));
            AddSymbol(new BasicSymbol("time_t", "long"));

            AddSymbol(new BasicSymbol("gsize", "ulong"));
            AddSymbol(new BasicSymbol("guint64", "ulong"));
            AddSymbol(new BasicSymbol("gulong", "ulong"));

            AddSymbol(new BasicSymbol("utf8", "string"));
            AddSymbol(new BasicSymbol("filename", "string"));
            // AddBasicSymbol(new BasicSymbol("Window", "ulong"));
        }

        /// <summary>
        /// Adds a symbol under the given namespace.
        /// </summary>
        /// <param name="nspace"></param>
        /// <param name="info"></param>
        public void AddSymbol(string nspace, ISymbol info)
        {
            GetTypeDict(nspace).AddSymbol(info.NativeName, info);
        }

        public void AddSymbols(string nspace, IEnumerable<ISymbol> infos)
        {
            TypeDictionary dict = GetTypeDict(nspace);
            foreach (ISymbol info in infos)
                dict.AddSymbol(info.NativeName, info);
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

        private void AddSymbol(ISymbol symbol)
        {
            _defaultDict.AddSymbol(symbol.NativeName, symbol);
        }

        private ISymbol GetSymbolInternal(TypeDictionary symbolDict, string symbol)
        {
            // Check Fundamental Types
            if (_defaultDict.TryGetSymbol(symbol, out ISymbol? info))
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
        public ISymbol GetSymbol(string? nspace, string symbol)
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
