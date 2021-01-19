using System.Collections.Generic;
using System.Threading.Tasks;

using Generator.Analysis;
using Generator.Introspection;
using Generator.Services;

namespace Generator
{
    public class Generator
    {
        private readonly TypeDictionary TypeDict = new();
        private readonly List<LoadedProject> LoadedProjects = new();
        private readonly ServiceManager ServiceManager;

        private const string GENERATOR_VERSION = "0.2.0"; 
        
        /// <summary>
        /// The main interface used to generate source files from GObject
        /// Introspection (GIR) data. Create a Generator object and then
        /// call <see cref="WriteAsync"/> in order to output generated code.
        /// </summary>
        /// <param name="projects"></param>
        public Generator(Project[] projects)
        {
            Log.Information($"Initialising generator with {projects.Length} toplevel project(s)");
            
            // Loading - Serialise all gir files into the LoadedProjects
            // dictionary. We attempt to continue regardless of whether all
            // files are loaded successfully.
            var loader = new Loader(projects, "../gir-files");
            
            // Sorts dependencies in order of base -> top level. Will throw if
            // a circular dependency is found.
            LoadedProjects = loader.GetOrderedList();
            
            // Processing - We need to transform raw gir data to create a more
            // ergonomic C# API. This includes prefixing interfaces, storing rich
            // type information, etc
            
            TypeDict = new TypeDictionary();

            // Process symbols (Add delegate to hook into symbol processing)
            foreach (LoadedProject proj in LoadedProjects)
            {
                GNamespace nspace = proj.Repository.Namespace;
                Log.Information($"Reading symbols for library '{proj.ProjectName}'");

                // TODO: Add, then Process?
                
                // Add Objects
                foreach (GClass cls in nspace?.Classes)
                    AddClassSymbol(nspace, cls);

                foreach (GInterface iface in nspace?.Interfaces)
                    AddInterfaceSymbol(nspace, iface);
            }
            
            // Create service manager with our loaded symbol dictionary
            ServiceManager = new ServiceManager(TypeDict);
            
            // Add services
            ServiceManager.Add(new ObjectService());

            Log.Information("Finished");
        }

        // TODO: Move these to an analyse module
        private void AddInterfaceSymbol(GNamespace nspace, GInterface iface)
        {
            var nativeName = new QualifiedName(nspace.Name, iface.Name);
            var managedName = new QualifiedName(nspace.Name, iface.Name);

            var symbol = new InterfaceSymbol(nativeName, managedName, iface);
            
            // Opportunity for user to transform non-fixed data

            // Prefix interface names with 'I'
            symbol.ManagedName.Type = "I" + symbol.ManagedName.Type;

            TypeDict.AddSymbol(symbol);
        }

        private void AddClassSymbol(GNamespace nspace, GClass cls)
        {
            var nativeName = new QualifiedName(nspace.Name, cls.Name);
            var managedName = new QualifiedName(nspace.Name, cls.Name);

            var symbol = new ObjectSymbol(nativeName, managedName, cls);
            
            // Opportunity for user to transform non-fixed data

            TypeDict.AddSymbol(symbol);
        }

        // TODO: Add more configuration options to Writer
        public async Task<int> WriteAsync()
        {
            var writer = new Writer(ServiceManager, TypeDict, LoadedProjects);
            return await writer.WriteAsync();
        }
    }
}
