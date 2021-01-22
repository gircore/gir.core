using System;
using Repository.Model;

namespace Repository.Analysis
{
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
        public ISymbol LookupSymbol(string symbolName)
        {
            if (string.IsNullOrEmpty(symbolName))
                throw new ArgumentNullException(nameof(symbolName), "Provided lookup cannot be null or empty");
            
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
