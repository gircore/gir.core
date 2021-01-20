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
                NamespaceInfo nspace = proj.Repository.Namespace;
                Log.Information($"Reading symbols for library '{proj.ProjectName}'");

                // Register Aliases
                foreach (AliasInfo alias in nspace?.Aliases ?? Enumerable.Empty<AliasInfo>())
                    AddAlias(nspace, alias);
                
                // Register Symbols
                foreach (ClassInfo cls in nspace?.Classes ?? Enumerable.Empty<ClassInfo>())
                    AddClassSymbol(nspace, cls);

                foreach (InterfaceInfo iface in nspace?.Interfaces ?? Enumerable.Empty<InterfaceInfo>())
                    AddInterfaceSymbol(nspace, iface);
                
                foreach (CallbackInfo dlg in nspace?.Callbacks ?? Enumerable.Empty<CallbackInfo>())
                    AddDelegateSymbol(nspace, dlg);
                
                foreach (EnumInfo @enum in nspace?.Enumerations ?? Enumerable.Empty<EnumInfo>())
                    AddEnumSymbol(nspace, @enum, false);
                
                foreach (EnumInfo @enum in nspace?.Bitfields ?? Enumerable.Empty<EnumInfo>())
                    AddEnumSymbol(nspace, @enum, true);
                
                foreach (RecordInfo @record in nspace?.Records ?? Enumerable.Empty<RecordInfo>())
                    AddRecordSymbol(nspace, @record);
            }

            Log.Information("Ready to Write");
        }

        // TODO: Move these to an analyse module
        private void AddInterfaceSymbol(NamespaceInfo nspace, InterfaceInfo iface)
        {
            var nativeName = new QualifiedName(nspace.Name, iface.Name);
            var managedName = new QualifiedName(nspace.Name, iface.Name);

            var symbol = new InterfaceSymbol(nativeName, managedName, iface);
            
            // Opportunity for user to transform non-fixed data

            // Prefix interface names with 'I'
            symbol.ManagedName.Type = "I" + symbol.ManagedName.Type;

            TypeDict.AddSymbol(symbol);
        }
        
        private void AddEnumSymbol(NamespaceInfo nspace, EnumInfo @enum, bool isBitfield)
        {
            var nativeName = new QualifiedName(nspace.Name, @enum.Name);
            var managedName = new QualifiedName(nspace.Name, @enum.Name);

            var symbol = new EnumSymbol(nativeName, managedName, @enum, isBitfield);
            
            // Opportunity for user to transform non-fixed data

            TypeDict.AddSymbol(symbol);
        }

        private void AddRecordSymbol(NamespaceInfo nspace, RecordInfo @record)
        {
            var nativeName = new QualifiedName(nspace.Name, @record.Name);
            var managedName = new QualifiedName(nspace.Name, @record.Name);

            var symbol = new RecordSymbol(nativeName, managedName, @record);
            
            // Opportunity for user to transform non-fixed data

            TypeDict.AddSymbol(symbol);
        }

        private void AddClassSymbol(NamespaceInfo nspace, ClassInfo cls)
        {
            var nativeName = new QualifiedName(nspace.Name, cls.Name);
            var managedName = new QualifiedName(nspace.Name, cls.Name);

            var symbol = new ObjectSymbol(nativeName, managedName, cls);
            
            // Opportunity for user to transform non-fixed data

            TypeDict.AddSymbol(symbol);
        }
        
        private void AddDelegateSymbol(NamespaceInfo nspace, CallbackInfo dlg)
        {
            var nativeName = new QualifiedName(nspace.Name, dlg.Name);
            var managedName = new QualifiedName(nspace.Name, dlg.Name);

            var symbol = new DelegateSymbol(nativeName, managedName, dlg);
            
            // Opportunity for user to transform non-fixed data

            // Suffix with 'Native'
            symbol.ManagedName.Type += "Native";

            TypeDict.AddSymbol(symbol);
        }
        
        private void AddAlias(NamespaceInfo nspace, AliasInfo aliasInfo)
        {
            var aliasName = new QualifiedName(nspace.Name, aliasInfo.Name);

            QualifiedName targetName;
            if (aliasInfo.For!.Name!.Contains('.'))
            {
                var components = aliasInfo.For.Name.Split('.', 2);
                targetName = new QualifiedName(components[0], components[1]);
            }
            else
                targetName = new QualifiedName(nspace.Name, aliasInfo.For.Name);

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
