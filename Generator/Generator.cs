using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Generator.Analysis;
using Generator.Introspection;
using Scriban;

namespace Generator
{
    public class Generator
    {
        public readonly TypeDictionary TypeDict = new();
        public readonly List<LoadedProject> LoadedProjects = new();

        private const string GENERATOR_VERSION = "0.2.0"; 
        
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
            
            // TODO: Services/Codegen

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

        public async Task<int> WriteAsync()
        {
            List<Task> AsyncTasks = new();
            
            foreach (LoadedProject proj in LoadedProjects)
            {
                GNamespace nspace = proj.Repository.Namespace;

                // Generate Asynchronously
                AsyncTasks.Add(WriteObjects(proj, nspace));
            }
            
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

        private string TestServiceWriteInheritance(ObjectSymbol obj)
        {
            GClass classInfo = obj.ClassInfo;
            if (classInfo.Parent is null)
                return string.Empty;

            // Parent Object
            var result = $": {classInfo.Parent}";

            // Interfaces
            foreach (GImplement impl in classInfo.Implements)
            {
                InterfaceSymbol ifaceSymbol;
                
                // If the name contains a dot, it is qualified
                // TODO: Make a 'IsQualified()' method
                if (impl.Name.Contains('.'))
                    ifaceSymbol = (InterfaceSymbol) TypeDict.GetSymbol(impl.Name);
                else
                    ifaceSymbol = (InterfaceSymbol) TypeDict.GetSymbol(obj.NativeName.Namespace, impl.Name);
                
                result += $", {ifaceSymbol.ManagedName}";
            }

            return result;
        }

        public async Task WriteObjects(LoadedProject proj, GNamespace nspace)
        {
            // Read generic template
            var objTemplate = ReadTemplate("object.sbntxt");
            var template = Template.Parse(objTemplate);
            
            // Create Directory
            var dir = $"output/{proj.ProjectName}/Classes/";
            Directory.CreateDirectory(dir);
            
            // Generate a file for each class
            foreach (GClass cls in nspace?.Classes ?? new List<GClass>())
            {
                // Skip GObject, GInitiallyUnowned
                if (cls.Name == "Object" || cls.Name == "InitiallyUnowned")
                    continue;
                
                var symbolInfo = (ObjectSymbol)TypeDict.GetSymbol(nspace.Name, cls.Name);

                Debug.Assert(symbolInfo.ClassInfo == cls, "SymbolInfo/GClass mismatch");

                // These contain: Object, Signals, Fields, Native: {Properties, Methods}
                var result = await template.RenderAsync(new
                {
                    Namespace = nspace.Name,
                    Name = symbolInfo.ManagedName.Type,
                    Inheritance = TestServiceWriteInheritance(symbolInfo),
                });

                var path = Path.Combine(dir, $"{cls.Name}.Generated.cs");
                File.WriteAllText(path, result);
            }
        }

        private static string ReadTemplate(string resource)
        {
            Stream? stream = Assembly.GetExecutingAssembly()
                .GetManifestResourceStream($"Generator.Templates.{resource}");

            if (stream == null)
                throw new IOException($"Cannot get template resource file '{resource}'");

            using var reader = new StreamReader(stream);
            return reader.ReadToEnd();
        }
    }
}
