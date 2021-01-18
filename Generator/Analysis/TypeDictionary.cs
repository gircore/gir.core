using System;
using System.Collections.Generic;

namespace Generator.Analysis
{
    public class TypeDictionary
    {
        private readonly Dictionary<QualifiedName, SymbolInfo> _typeDict = new();

        public void AddSymbol(SymbolInfo info)
        {
            _typeDict.Add(info.nativeName, info);
        }

        public SymbolInfo GetSymbol(QualifiedName nativeName)
            => _typeDict[nativeName];

        public SymbolInfo GetSymbol(string nspace, string type)
        {
            if (nspace.Contains('.'))
                throw new ArgumentException("Provided string should not contain '.' character", nameof(nspace));
            
            if (type.Contains('.'))
                throw new ArgumentException("Provided string should not contain '.' character", nameof(type));
            
            return GetSymbol(new QualifiedName(nspace, type));
        }
        
        public SymbolInfo GetSymbol(string qualifiedName)
        {
            if (!qualifiedName.Contains('.'))
                throw new ArgumentException("Provided string must be in format 'Namespace.Type': e.g. 'Gdk.Screen'", nameof(qualifiedName));
            
            var components = qualifiedName.Split('.');
            return GetSymbol(components[0], components[1]);
        }
    }
}
