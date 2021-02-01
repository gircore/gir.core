using System;
using Repository.Model;

namespace Repository.Analysis
{
    // A view of the type dictionary from a specific namespace (Immutable access only)
    public class SymbolDictionaryView
    {
        public SymbolDictionary SymbolDict { get; }
        public string Namespace { get; }
        
        public SymbolDictionaryView(SymbolDictionary symbolDict, string nspace)
        {
            SymbolDict = symbolDict;
            Namespace = nspace;
        }

        /// <summary>
        /// Looks up <see cref="typeName"/> from the perspective of the
        /// <see cref="SymbolDictionaryView"/>'s namespace. Unqualified types
        /// are looked up in the current namespace, while Qualified types
        /// are searched globally. Fundamental types take priority over any
        /// other type name.
        /// </summary>
        /// <param name="typeName">Name of the symbol. May be qualified or unqualified</param>
        /// <returns>Information about the symbol</returns>
        public IType LookupType(string typeName)
        {
            if (string.IsNullOrEmpty(typeName))
                throw new ArgumentNullException(nameof(typeName), "Provided lookup cannot be null or empty");
            
            if (typeName.Contains('.'))
            {
                // We are in the form 'Namespace.Type'
                var components = typeName.Split('.', 2);
                return SymbolDict.GetType(components[0], components[1]);
            }

            // We are not qualified by a namespace, so assume this one.
            // It might also be a fundamental type, but the type dict
            // takes care of this.
            return SymbolDict.GetType(Namespace, typeName);
        }
    }
}
