using System;
using System.Collections.Generic;

namespace Generator.Analysis
{
    public class TypeDictionary
    {
        private readonly Dictionary<QualifiedName, ISymbolInfo> _typeDict = new();
        private readonly Dictionary<QualifiedName, QualifiedName> _aliasDict = new();

        public TypeDictionary()
        {
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
            
            // AddSymbol(new BasicSymbol("cairo_format_t", "int"));
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
            // AddSymbol(new BasicSymbol("GQuark", "uint"));
            
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
            // AddSymbol(new BasicSymbol("Window", "ulong"));
        }

        public void AddSymbol(ISymbolInfo info)
        {
            _typeDict.Add(info.NativeName, info);
        }

        public void AddAlias(QualifiedName from, QualifiedName to)
        {
            _aliasDict.Add(from, to);
        }

        private ISymbolInfo GetSymbolInternal(QualifiedName nativeName)
        {
            // TODO: Ugly hack to deal with basic types in the meantime
            // Check first for non-namespaced (i.e. Fundamental types), then
            // for namespaced types. We want a single interface for GetSymbol.
            if (_typeDict.TryGetValue(new QualifiedName(String.Empty, nativeName.Type), out ISymbolInfo info))
                return info;
            
            return _typeDict[nativeName];   
        }

        public ISymbolInfo GetSymbol(QualifiedName nativeName)
        {
            // First Check Aliases
            if (_aliasDict.TryGetValue(nativeName, out QualifiedName aliasedName))
                return GetSymbolInternal(aliasedName);
            
            // Then try get symbol
            return GetSymbolInternal(nativeName);
        }

        public ISymbolInfo GetSymbol(string nspace, string type)
        {
            if (type.Contains('.'))
            {
                var components = type.Split('.', 2);
                return GetSymbol(new QualifiedName(components[0], components[1]));
            }

            return GetSymbol(new QualifiedName(nspace, type));
        }
    }
}
