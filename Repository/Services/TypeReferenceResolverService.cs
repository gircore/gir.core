using System.Collections.Generic;
using Repository.Analysis;
using Repository.Model;

namespace Repository.Services
{
    public interface ITypeReferenceResolverService
    {
        void Resolve(IEnumerable<ILoadedProject> projects);
    }

    public class TypeReferenceResolverService : ITypeReferenceResolverService
    {
        public void Resolve(IEnumerable<ILoadedProject> projects)
        {
            var symbolDictionary = new SymbolDictionary();

            foreach (var proj in projects)
            {
                Log.Information($"Analysing '{proj.Name}'.");

                AddAliases(symbolDictionary, proj.Namespace);
                AddClasses(symbolDictionary, proj.Namespace);
                AddInterfaces(symbolDictionary, proj.Namespace);
            }
        }

        private void AddAliases(SymbolDictionary symbolDictionary, Namespace @namespace)
        {
            foreach (Alias alias in @namespace.Aliases)
                symbolDictionary.AddAlias(@namespace.Name, alias.From, alias.To);
        }

        private void AddClasses(SymbolDictionary symbolDictionary, Namespace @namespace)
        {
            foreach (Class cls in @namespace.Classes)
                symbolDictionary.AddType(@namespace.Name, cls);
        }
        
        private void AddInterfaces(SymbolDictionary symbolDictionary, Namespace @namespace)
        {
            foreach (Interface iface in @namespace.Interfaces)
                symbolDictionary.AddType(@namespace.Name, iface);
        }
    }
}
