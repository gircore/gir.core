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
                var symbol = view.LookupType(reference.Type);
                reference.ResolveAs(symbol);
            }
        }

        private static void AddAliases(SymbolDictionary symbolDictionary, Namespace @namespace)
        {
            foreach (var alias in @namespace.Aliases)
                symbolDictionary.AddAlias(@namespace.Name, alias.Name, alias.ManagedName);
        }
    }
}
