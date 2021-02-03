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

            AddBasicType(new BasicType("none", "void"));
            AddBasicType(new BasicType("any", "IntPtr"));

            AddBasicType(new BasicType("void", "void"));
            AddBasicType(new BasicType("gboolean", "bool"));
            AddBasicType(new BasicType("gfloat", "float"));
            AddBasicType(new BasicType("float", "float"));

            AddBasicType(new BasicType("gconstpointer", "IntPtr"));
            AddBasicType(new BasicType("va_list", "IntPtr"));
            AddBasicType(new BasicType("gpointer", "IntPtr"));
            AddBasicType(new BasicType("GType", "IntPtr"));
            AddBasicType(new BasicType("tm", "IntPtr"));

            AddBasicType(new BasicType("guint16", "ushort"));
            AddBasicType(new BasicType("gushort", "ushort"));

            AddBasicType(new BasicType("gint16", "short"));
            AddBasicType(new BasicType("gshort", "short"));

            AddBasicType(new BasicType("double", "double"));
            AddBasicType(new BasicType("gdouble", "double"));
            AddBasicType(new BasicType("long double", "double"));

            // AddBasicSymbol(new BasicSymbol("cairo_format_t", "int"));
            AddBasicType(new BasicType("int", "int"));
            AddBasicType(new BasicType("gint", "int"));
            AddBasicType(new BasicType("gint32", "int"));
            AddBasicType(new BasicType("pid_t", "int"));

            AddBasicType(new BasicType("unsigned int", "uint"));
            AddBasicType(new BasicType("unsigned", "uint"));
            AddBasicType(new BasicType("guint", "uint"));
            AddBasicType(new BasicType("guint32", "uint"));
            AddBasicType(new BasicType("gunichar", "uint"));
            AddBasicType(new BasicType("uid_t", "uint"));
            // AddBasicSymbol(new BasicSymbol("GQuark", "uint"));

            AddBasicType(new BasicType("guchar", "byte"));
            AddBasicType(new BasicType("gchar", "byte"));
            AddBasicType(new BasicType("char", "byte"));
            AddBasicType(new BasicType("guint8", "byte"));
            AddBasicType(new BasicType("gint8", "byte"));

            AddBasicType(new BasicType("glong", "long"));
            AddBasicType(new BasicType("gssize", "long"));
            AddBasicType(new BasicType("gint64", "long"));
            AddBasicType(new BasicType("goffset", "long"));
            AddBasicType(new BasicType("time_t", "long"));

            AddBasicType(new BasicType("gsize", "ulong"));
            AddBasicType(new BasicType("guint64", "ulong"));
            AddBasicType(new BasicType("gulong", "ulong"));

            AddBasicType(new BasicType("utf8", "string"));
            AddBasicType(new BasicType("filename", "string"));
            // AddBasicSymbol(new BasicSymbol("Window", "ulong"));
        }

        /// <summary>
        /// Adds a symbol under the given namespace.
        /// </summary>
        /// <param name="nspace"></param>
        /// <param name="info"></param>
        public void AddType(string nspace, IType info)
        {
            GetTypeDict(nspace).AddType(info.NativeName, info);
        }

        public void AddTypes(string nspace, IEnumerable<IType> infos)
        {
            TypeDictionary dict = GetTypeDict(nspace);
            foreach (IType info in infos)
                dict.AddType(info.NativeName, info);
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

        private void AddBasicType(BasicType basicType)
        {
            _defaultDict.AddType(basicType.NativeName, basicType);
        }

        private IType GetTypeInternal(TypeDictionary symbolDict, string symbol)
        {
            // Check Fundamental Types
            if (_defaultDict.TryGetType(symbol, out IType? info))
                return info;

            // Check Normal
            return symbolDict.GetType(symbol);
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
        public IType GetType(string? nspace, string symbol)
        {
            Debug.Assert(
                condition: !symbol.Contains('.'),
                message: $"Symbol {symbol} should not be qualified by a namespace"
            );

            if (nspace == null)
                return _defaultDict.GetType(symbol);

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
                    return GetType(nspace: components[0], symbol: components[1]);
                }

                // Within this namespace
                return GetTypeInternal(symbolDict, alias);
            }

            // Check Normal
            return GetTypeInternal(symbolDict, symbol);
        }
    }
}
