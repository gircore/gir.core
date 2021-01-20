using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

using Generator.Analysis;
using Generator.Introspection;
using Generator.Services;

using Scriban;

namespace Generator
{
    public class Writer
    {
        public readonly TypeDictionaryView TypeDict;
        
        public readonly ServiceManager ServiceManager;
        public readonly LoadedProject Project;
        public readonly NamespaceInfo Namespace;
        public string CurrentNamespace => Namespace.Name;

        public Writer(LoadedProject project, TypeDictionaryView typeDict)
        {
            TypeDict = typeDict;
            Project = project;
            Namespace = project.Repository!.Namespace;
            
            // Create service manager with our loaded symbol dictionary
            ServiceManager = new ServiceManager(TypeDict, Namespace!.Name);
            
            // Add services
            ServiceManager.Add(new ObjectService());
            ServiceManager.Add(new UncategorisedService());
        }

        public IEnumerable<Task> GetAsyncTasks()
        {
            List<Task> asyncTasks = new();
            
            asyncTasks.Add(WriteObjectFiles());
            asyncTasks.Add(WriteDelegateFiles());

            return asyncTasks;
        }

        private async Task WriteObjectFiles()
        {
            // Read generic template
            var objTemplate = ReadTemplate("object.sbntxt");
            var template = Template.Parse(objTemplate);
            
            // Create Directory
            var dir = $"output/{Project.ProjectName}/Classes/";
            Directory.CreateDirectory(dir);
            
            // Generate a file for each class
            foreach (ClassInfo cls in Namespace.Classes ?? new List<ClassInfo>())
            {
                // Skip GObject, GInitiallyUnowned
                if (cls.Name == "Object" || cls.Name == "InitiallyUnowned")
                    continue;
                
                var symbolInfo = (ObjectSymbol)TypeDict.LookupSymbol(cls.Name);

                Debug.Assert(symbolInfo.ClassInfo == cls, "SymbolInfo/GClass mismatch");

                // These contain: Object, Signals, Fields, Native: {Properties, Methods}
                var result = await template.RenderAsync(new
                {
                    Namespace = CurrentNamespace,
                    Name = symbolInfo.ManagedName.Type,
                    Inheritance = ServiceManager.Get<ObjectService>().WriteInheritance(symbolInfo),
                    TypeName = cls.TypeName,
                });

                var path = Path.Combine(dir, $"{cls.Name}.Generated.cs");
                await File.WriteAllTextAsync(path, result);
            }
        }
        
        private async Task WriteDelegateFiles()
        {
            // Read generic template
            var dlgTemplate = ReadTemplate("delegate.sbntxt");
            var template = Template.Parse(dlgTemplate);
            
            // Create Directory
            var dir = $"output/{Project.ProjectName}/Delegates/";
            Directory.CreateDirectory(dir);
            
            // Generate a file for each class
            foreach (CallbackInfo dlg in Namespace?.Callbacks ?? new List<CallbackInfo>())
            {
                var dlgSymbol = (DelegateSymbol)TypeDict.LookupSymbol(dlg.Name);

                var result = await template.RenderAsync(new
                {
                    Namespace = CurrentNamespace,
                    ReturnValue = ServiceManager.Get<UncategorisedService>().WriteReturnValue(dlgSymbol),
                    WrapperType = dlg.Name,
                    WrappedType = dlgSymbol.ManagedName.Type,
                    ManagedParameters = ServiceManager.Get<UncategorisedService>().WriteParameters(dlgSymbol),
                });

                var path = Path.Combine(dir, $"{dlg.Name}.Generated.cs");
                await File.WriteAllTextAsync(path, result);
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
