using System.Collections.Generic;
using System.Linq;
using Repository.Analysis;
using Repository.Model;

namespace Repository.Services
{
    internal class TypeReferenceResolverService 
    {
        public static void Resolve(IEnumerable<LoadedProject> projects)
        {
            var symbolDictionary = new SymbolDictionary2();

            var loadedProjects = projects.ToList();
            foreach (var proj in loadedProjects)
            {
                Log.Debug($"Analysing '{proj.Name}'.");
                FillSymbolDictionary(symbolDictionary, proj.Namespace);
                
                ResolveReferences(symbolDictionary, proj);
                Log.Information($"Resolved symbol references for {proj.Name}.");
            }
        }

        private static void FillSymbolDictionary(SymbolDictionary2 symbolDictionary, Namespace @namespace)
        {
            symbolDictionary.AddSymbols(@namespace.Aliases);
            symbolDictionary.AddSymbols(@namespace.Classes);
            symbolDictionary.AddSymbols(@namespace.Interfaces);
            symbolDictionary.AddSymbols(@namespace.Callbacks);
            symbolDictionary.AddSymbols(@namespace.Enumerations);
            symbolDictionary.AddSymbols(@namespace.Bitfields);
            symbolDictionary.AddSymbols(@namespace.Records);
            symbolDictionary.AddSymbols(@namespace.Unions);
        }

        private static void ResolveReferences(SymbolDictionary2 symbolDictionary, LoadedProject proj)
        {
            foreach (var reference in proj.Namespace.GetSymbolReferences())
            {
                Resolve(symbolDictionary, reference);
            }
        }

        private static void Resolve(SymbolDictionary2 symbolDictionary, SymbolReference reference)
        {
            if(symbolDictionary.TryLookup(reference, out var symbol))
                reference.ResolveAs(symbol);
        }
    }
}
