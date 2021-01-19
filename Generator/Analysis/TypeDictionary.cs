using System;
using System.Collections.Generic;

namespace Generator.Analysis
{
    public class TypeDictionary
    {
        private readonly Dictionary<QualifiedName, ISymbolInfo> _typeDict = new();

        public void AddSymbol(ISymbolInfo info)
        {
            _typeDict.Add(info.NativeName, info);
        }

        public ISymbolInfo GetSymbol(QualifiedName nativeName)
            => _typeDict[nativeName];

        public ISymbolInfo GetSymbol(string nspace, string type)
        {
            if (nspace.Contains('.'))
                throw new ArgumentException("Provided string should not contain '.' character", nameof(nspace));
            
            if (type.Contains('.'))
                throw new ArgumentException("Provided string should not contain '.' character", nameof(type));
            
            return GetSymbol(new QualifiedName(nspace, type));
        }
        
        public ISymbolInfo GetSymbol(string qualifiedName)
        {
            if (!qualifiedName.Contains('.'))
                throw new ArgumentException("Provided string must be in format 'Namespace.Type': e.g. 'Gdk.Screen'", nameof(qualifiedName));
            
            var components = qualifiedName.Split('.');
            return GetSymbol(components[0], components[1]);
        }
    }
}
