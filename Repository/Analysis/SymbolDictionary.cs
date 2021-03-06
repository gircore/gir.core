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

            AddSymbol(Symbol.Primitive("none", "void"));
            AddSymbol(Symbol.Primitive("any", "IntPtr"));

            AddSymbol(Symbol.Primitive("void", "void"));
            AddSymbol(Symbol.Primitive("gboolean", "bool"));
            AddSymbol(Symbol.Primitive("gfloat", "float"));
            AddSymbol(Symbol.Primitive("float", "float"));

            AddSymbol(Symbol.Primitive("gconstpointer", "IntPtr"));
            AddSymbol(Symbol.Primitive("va_list", "IntPtr"));
            AddSymbol(Symbol.Primitive("gpointer", "IntPtr"));
            AddSymbol(Symbol.Primitive("GType", "IntPtr"));
            AddSymbol(Symbol.Primitive("tm", "IntPtr"));
            
            // TODO: Should we use UIntPtr here? Non-CLR compliant
            AddSymbol(Symbol.Primitive("guintptr", "UIntPtr"));

            AddSymbol(Symbol.Primitive("guint16", "ushort"));
            AddSymbol(Symbol.Primitive("gushort", "ushort"));

            AddSymbol(Symbol.Primitive("gint16", "short"));
            AddSymbol(Symbol.Primitive("gshort", "short"));

            AddSymbol(Symbol.Primitive("double", "double"));
            AddSymbol(Symbol.Primitive("gdouble", "double"));
            AddSymbol(Symbol.Primitive("long double", "double"));

            // AddBasicSymbol(new BasicSymbol("cairo_format_t", "int"));
            AddSymbol(Symbol.Primitive("int", "int"));
            AddSymbol(Symbol.Primitive("gint", "int"));
            AddSymbol(Symbol.Primitive("gint32", "int"));
            AddSymbol(Symbol.Primitive("pid_t", "int"));

            AddSymbol(Symbol.Primitive("unsigned int", "uint"));
            AddSymbol(Symbol.Primitive("unsigned", "uint"));
            AddSymbol(Symbol.Primitive("guint", "uint"));
            AddSymbol(Symbol.Primitive("guint32", "uint"));
            AddSymbol(Symbol.Primitive("gunichar", "uint"));
            AddSymbol(Symbol.Primitive("uid_t", "uint"));
            // AddBasicSymbol(new BasicSymbol("GQuark", "uint"));

            AddSymbol(Symbol.Primitive("guchar", "byte"));
            AddSymbol(Symbol.Primitive("gchar", "sbyte"));
            AddSymbol(Symbol.Primitive("char", "sbyte"));
            AddSymbol(Symbol.Primitive("guint8", "byte"));
            AddSymbol(Symbol.Primitive("gint8", "sbyte"));

            AddSymbol(Symbol.Primitive("glong", "long"));
            AddSymbol(Symbol.Primitive("gssize", "long"));
            AddSymbol(Symbol.Primitive("gint64", "long"));
            AddSymbol(Symbol.Primitive("goffset", "long"));
            AddSymbol(Symbol.Primitive("time_t", "long"));

            AddSymbol(Symbol.Primitive("gsize", "ulong"));
            AddSymbol(Symbol.Primitive("guint64", "ulong"));
            AddSymbol(Symbol.Primitive("gulong", "ulong"));

            AddSymbol(Symbol.Primitive("utf8", "string"));
            AddSymbol(Symbol.Primitive("filename", "string"));
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
