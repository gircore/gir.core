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
        public ServiceManager ServiceManager;
        public TypeDictionary TypeDict;
        public List<LoadedProject> Projects = new();

        public Writer(ServiceManager services, TypeDictionary typeDict, IEnumerable<LoadedProject> projects)
        {
            TypeDict = typeDict;
            ServiceManager = services;
            Projects.AddRange(projects);
        }

        public async Task<int> WriteAsync()
        {
            List<Task> AsyncTasks = new();
            
            foreach (LoadedProject proj in Projects)
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
                    Inheritance = ServiceManager.Get<ObjectService>().WriteInheritance(symbolInfo),
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
