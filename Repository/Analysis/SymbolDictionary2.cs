using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using Repository.Model;

namespace Repository.Analysis
{
    public partial class SymbolDictionary2
    {
        private readonly Dictionary<Namespace, SymbolCache> _data = new();
        private readonly SymbolCache _globalSymbols = new();

        public SymbolDictionary2()
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
        
        public bool TryLookup(SymbolReference symbolReference, [MaybeNullWhen(false)] out Symbol symbol)
        {
            if (_globalSymbols.TryLookup(symbolReference, out symbol))
                return true;
        }
        
        public void AddSymbols(IEnumerable<Symbol> symbols)
        {
            foreach(var symbol in symbols)
            {
                if (symbol.Namespace is null)
                    AddDefaultSymbol(symbol);
                else
                    AddSymbol(symbol);
            }
        }

        private void AddDefaultSymbol(Symbol symbol)
        {
            _globalSymbols.Add(symbol);
        }

        private void AddSymbol(Symbol symbol)
        {
            Debug.Assert(
                condition: symbol.Namespace is not null, 
                message: "Symbol namespace is null"
            );
            
            if (!_data.TryGetValue(symbol.Namespace, out var cache))
            {
                cache = new SymbolCache();
                _data[symbol.Namespace] = cache;
            }

            cache.Add(symbol);
        }
    }
}
