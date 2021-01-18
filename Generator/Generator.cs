using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Generator.Analysis;
using Generator.Introspection;

namespace Generator
{
    public class Generator
    {
        public readonly Dictionary<Project, GRepository> LoadedProjects = new();
        public readonly TypeDictionary TypeDict = new();
        
        public Generator(Project[] projects)
        {
            Log.Information($"Initialising generator with {projects.Length} toplevel project(s)");
            
            // Loading - Serialise all gir files into the LoadedProjects
            // dictionary. We attempt to continue regardless of whether all
            // files are loaded successfully.
            var loader = new Loader(projects, "../gir-files");
            
            // Sorts dependencies in order of base -> top level. Will throw if
            // a circular dependency is found.
            List<LoadedProject> loadedProjects = loader.GetOrderedList();
            
            // Processing - We need to transform raw gir data to create a more
            // ergonomic C# API. This includes prefixing interfaces, storing rich
            // type information, etc
            
            TypeDict = new TypeDictionary();

            // Process symbols (Add delegate to hook into symbol processing)
            foreach (LoadedProject proj in loadedProjects)
            {
                GNamespace nspace = proj.Repository.Namespace;
                Log.Information($"Reading symbols for library '{nspace.Name}-{nspace.Version}' (provided by '{proj.ProjectData.Gir}')");

                // Add Objects
                foreach (GClass cls in nspace?.Classes)
                    AddClassSymbol(nspace, cls);

                foreach (GInterface iface in nspace?.Interfaces)
                    AddInterfaceSymbol(nspace, iface);
            }
            
            Log.Information("Finished");
        }

        // TODO: Move these to an analyse module
        private void AddInterfaceSymbol(GNamespace nspace, GInterface iface)
        {
            var nativeName = new QualifiedName(nspace.Name, iface.Name);
            var managedName = new QualifiedName(nspace.Name, iface.Name);
            var classification = Classification.Reference;

            var symbol = new SymbolInfo(SymbolType.Interface, nativeName, managedName, classification);
            
            // Opportunity for user to transform non-fixed data

            // Prefix interface names with 'I'
            symbol.managedName.type = "I" + symbol.managedName.type;

            TypeDict.AddSymbol(symbol);
        }

        private void AddClassSymbol(GNamespace nspace, GClass cls)
        {
            var nativeName = new QualifiedName(nspace.Name, cls.Name);
            var managedName = new QualifiedName(nspace.Name, cls.Name);
            var classification = Classification.Reference;

            var symbol = new SymbolInfo(SymbolType.Object, nativeName, managedName, classification);
            
            // Opportunity for user to transform non-fixed data

            TypeDict.AddSymbol(symbol);
        }

        public async Task<int> WriteAsync()
        {
            // foreach...
            return 0;
        }
    }
}
