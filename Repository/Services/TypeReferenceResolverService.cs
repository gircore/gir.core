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

            symbolDictionary.AddTypes(@namespace.Name, @namespace.Classes);
            symbolDictionary.AddTypes(@namespace.Name, @namespace.Interfaces);
            symbolDictionary.AddTypes(@namespace.Name, @namespace.Callbacks);
            symbolDictionary.AddTypes(@namespace.Name, @namespace.Enumerations);
            symbolDictionary.AddTypes(@namespace.Name, @namespace.Bitfields);
            symbolDictionary.AddTypes(@namespace.Name, @namespace.Records);
        }

        private void ResolveReferences(SymbolDictionary symbolDictionary, ILoadedProject proj)
        {
            var view = symbolDictionary.GetView(proj.Namespace.Name);

            foreach (var reference in proj.TypeReferences)
            {
                var symbol = view.LookupType(reference.Name);

                ReferenceType kind = (symbol?.Namespace?.Name == proj.Namespace.Name)
                    ? ReferenceType.Internal
                    : ReferenceType.External;

                if (reference is IResolveable resolveable)
                    resolveable.ResolveAs(symbol, kind);
            }
        }

        private void AddAliases(SymbolDictionary symbolDictionary, Namespace @namespace)
        {
            foreach (Alias alias in @namespace.Aliases)
                symbolDictionary.AddAlias(@namespace.Name, alias.From, alias.To);
        }
    }
}
