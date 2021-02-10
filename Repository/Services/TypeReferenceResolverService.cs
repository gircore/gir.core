using System.Collections.Generic;
using System.Linq;
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

            var loadedProjects = projects.ToList();
            foreach (var proj in loadedProjects)
            {
                Log.Information($"Analysing '{proj.Name}'.");
                FillSymbolDictionary(symbolDictionary, proj.Namespace);

                Log.Information("Resolving symbol references.");
                ResolveReferences(symbolDictionary, proj);
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

        private void ResolveReferences(SymbolDictionary symbolDictionary, ILoadedProject proj)
        {
            var view = symbolDictionary.GetView(proj.Namespace.Name);

            foreach (var reference in proj.SymbolReferences)
            {
                var symbol = view.LookupType(reference.Name);


                ReferenceType kind = symbol switch
                {
                    IType t when t.Namespace.Name == proj.Namespace.Name => ReferenceType.Internal,
                    _ => ReferenceType.External
                };

                if (reference is IResolveableSymbolReference resolveable)
                    resolveable.ResolveAs(symbol, kind);
            }
        }

        private static void AddAliases(SymbolDictionary symbolDictionary, Namespace @namespace)
        {
            foreach (var alias in @namespace.Aliases)
                symbolDictionary.AddAlias(@namespace.Name, alias.NativeName, alias.ManagedName);
        }
    }
}
