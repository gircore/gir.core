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
        private readonly TypeDictionary TypeDict = new();
        private readonly List<LoadedProject> LoadedProjects = new();

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

            // TODO: Add, then Process?
            // TODO: Add delegate to hook into symbol processing?
            
            foreach (LoadedProject proj in LoadedProjects)
            {
                GNamespace nspace = proj.Repository.Namespace;
                Log.Information($"Reading symbols for library '{proj.ProjectName}'");

                // Register Aliases
                foreach (GAlias alias in nspace?.Aliases ?? Enumerable.Empty<GAlias>())
                    AddAlias(nspace, alias);
                
                // Register Symbols
                foreach (GClass cls in nspace?.Classes ?? Enumerable.Empty<GClass>())
                    AddClassSymbol(nspace, cls);

                foreach (GInterface iface in nspace?.Interfaces ?? Enumerable.Empty<GInterface>())
                    AddInterfaceSymbol(nspace, iface);
                
                foreach (GCallback dlg in nspace?.Callbacks ?? Enumerable.Empty<GCallback>())
                    AddDelegateSymbol(nspace, dlg);
                
                foreach (GEnumeration @enum in nspace?.Enumerations ?? Enumerable.Empty<GEnumeration>())
                    AddEnumSymbol(nspace, @enum, false);
                
                foreach (GEnumeration @enum in nspace?.Bitfields ?? Enumerable.Empty<GEnumeration>())
                    AddEnumSymbol(nspace, @enum, true);
                
                foreach (GRecord @record in nspace?.Records ?? Enumerable.Empty<GRecord>())
                    AddRecordSymbol(nspace, @record);
            }

            Log.Information("Ready to Write");
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
        
        private void AddEnumSymbol(GNamespace nspace, GEnumeration @enum, bool isBitfield)
        {
            var nativeName = new QualifiedName(nspace.Name, @enum.Name);
            var managedName = new QualifiedName(nspace.Name, @enum.Name);

            var symbol = new EnumSymbol(nativeName, managedName, @enum, isBitfield);
            
            // Opportunity for user to transform non-fixed data

            TypeDict.AddSymbol(symbol);
        }

        private void AddRecordSymbol(GNamespace nspace, GRecord @record)
        {
            var nativeName = new QualifiedName(nspace.Name, @record.Name);
            var managedName = new QualifiedName(nspace.Name, @record.Name);

            var symbol = new RecordSymbol(nativeName, managedName, @record);
            
            // Opportunity for user to transform non-fixed data

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
        
        private void AddDelegateSymbol(GNamespace nspace, GCallback dlg)
        {
            var nativeName = new QualifiedName(nspace.Name, dlg.Name);
            var managedName = new QualifiedName(nspace.Name, dlg.Name);

            var symbol = new DelegateSymbol(nativeName, managedName, dlg);
            
            // Opportunity for user to transform non-fixed data

            // Suffix with 'Native'
            symbol.ManagedName.Type += "Native";

            TypeDict.AddSymbol(symbol);
        }
        
        private void AddAlias(GNamespace nspace, GAlias alias)
        {
            var aliasName = new QualifiedName(nspace.Name, alias.Name);

            QualifiedName targetName;
            if (alias.For!.Name!.Contains('.'))
            {
                var components = alias.For.Name.Split('.', 2);
                targetName = new QualifiedName(components[0], components[1]);
            }
            else
                targetName = new QualifiedName(nspace.Name, alias.For.Name);

            TypeDict.AddAlias(aliasName, targetName);
        }

        // TODO: Add more configuration options to Writer
        public int WriteAsync()
        {
            List<Task> AsyncTasks = new();

            foreach (LoadedProject proj in LoadedProjects)
                AsyncTasks.AddRange(new Writer(proj, TypeDict).GetAsyncTasks());

            try
            {
                Task.WaitAll(AsyncTasks.ToArray());
                Log.Information("Writing completed successfully");
                return 0;
            }
            catch (Exception e)
            {
                Log.Exception(e);
                Log.Error("An error occurred while writing files. Please save a copy of your log output and open an issue at: https://github.com/gircore/gir.core/issues/new");
                return -1;
            }
        }
    }
}
