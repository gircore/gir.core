using System.Collections.Generic;
using System.Linq;
using Repository.Analysis;
using Repository.Model;

namespace Repository.Services
{
    internal class TypeReferenceResolverService 
    {
        public void Resolve(IEnumerable<LoadedProject> projects)
        {
            var symbolDictionary = new SymbolDictionary();

            var loadedProjects = projects.ToList();
            foreach (var proj in loadedProjects)
            {
                Log.Debug($"Analysing '{proj.Name}'.");
                FillSymbolDictionary(symbolDictionary, proj.Namespace);
                
                ResolveReferences(symbolDictionary, proj);
                Log.Information($"Resolved symbol references for {proj.Name}.");

                Strip();
                Log.Information($"Stripped unresolved symbols.");
            }
        }

        private void FillSymbolDictionary(SymbolDictionary symbolDictionary, Namespace @namespace)
        {
            AddAliases(symbolDictionary, @namespace);

            symbolDictionary.AddSymbols(@namespace.Name, @namespace.Classes);
            symbolDictionary.AddSymbols(@namespace.Name, @namespace.Interfaces);
            symbolDictionary.AddSymbols(@namespace.Name, @namespace.Callbacks);
            symbolDictionary.AddSymbols(@namespace.Name, @namespace.Enumerations);
            symbolDictionary.AddSymbols(@namespace.Name, @namespace.Bitfields);
            symbolDictionary.AddSymbols(@namespace.Name, @namespace.Records);
            symbolDictionary.AddSymbols(@namespace.Name, @namespace.Unions);
        }

        private void ResolveReferences(SymbolDictionary symbolDictionary, LoadedProject proj)
        {
            var view = symbolDictionary.GetView(proj.Namespace.Name);

            foreach (var reference in proj.Namespace.GetSymbolReferences())
            {
                Resolve(view, reference);
            }
        }

        private static void Resolve(SymbolDictionaryView view, SymbolReference reference)
        {
            var type = reference.Type
                .Replace("*", "")
                .Replace("const ", "")
                .Replace("volatile ", "")
                .Replace(" const", "");

            if (view.LookupType(type, out Symbol? symbol))
                reference.ResolveAs(symbol);
        }

        private static void AddAliases(SymbolDictionary symbolDictionary, Namespace @namespace)
        {
            foreach (var alias in @namespace.Aliases)
                symbolDictionary.AddAlias(@namespace.Name, alias.CName ?? alias.Name, alias.ManagedName);
        }

        private void Strip()
        {
            //TODO:
            // - Log and invalidate all methods which have unresolved member / return
            // - Log and invalidate all properties which are unresolved
            // - Log and invalidate all signals which are unresolved
            // - Log and invalidate all callbacks which are unresolved
            // - Log and invalidate all classes which have unresolved parents
            // - Log and invalidate all interfaces which have unresolved parents
            // - Log and invalidate all structs which have unresolved fields
            // - ...
            //
            // A symbol is only allowed to be generated if it is not invalidated
        }
    }
}
