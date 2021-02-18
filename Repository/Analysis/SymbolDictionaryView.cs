using System;
using Repository.Model;

namespace Repository.Analysis
{
    /// <summary>
    /// A TypeDictionaryView allows us to query qualified and unqualified names
    /// from the perspective of a given namespace. Unqualified names are assumed to be defined in the current namespace. For project 'Gtk':
    ///  - 'Application'     resolves to 'Gtk.Application' (Internal)
    ///  - 'Gio.Application' resolves to 'Gio.Application' (External)
    /// </summary>
    internal class SymbolDictionaryView
    {
        private readonly SymbolDictionary _symbolDict;
        private readonly string _namespace;
        
        public SymbolDictionaryView(SymbolDictionary symbolDict, string nspace)
        {
            _symbolDict = symbolDict;
            _namespace = nspace;
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
        public Symbol LookupType(string typeName)
        {
            if (string.IsNullOrEmpty(typeName))
                throw new ArgumentNullException(nameof(typeName), "Provided lookup cannot be null or empty");
            
            if (typeName.Contains('.'))
            {
                // We are in the form 'Namespace.Type'
                var components = typeName.Split('.', 2);
                return _symbolDict.GetSymbol(components[0], components[1]);
            }

            // We are not qualified by a namespace, so assume this one.
            // It might also be a fundamental type, but the type dict
            // takes care of this.
            return _symbolDict.GetSymbol(_namespace, typeName);
        }
    }
}
