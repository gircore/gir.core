using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using System.Collections.Generic;

using Repository;
using Repository.Model;

using Scriban;

using Generator.Services;

namespace Generator
{
    public class Writer
    {
        public readonly LoadedProject Project;
        public readonly Namespace Namespace;
        public string CurrentNamespace => Namespace.Name;
        
        // Services
        private readonly ObjectService _objectService;
        private readonly UncategorisedService _uncategorisedService;

        public Writer(LoadedProject project)
        {
            Project = project;
            Namespace = project.Namespace;
            
            // Add services
            _objectService = new ObjectService();
            _uncategorisedService = new UncategorisedService();
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
            foreach (Class cls in Namespace.Classes)
            {
                // Skip GObject, GInitiallyUnowned
                if (cls.NativeName == "Object" || cls.NativeName == "InitiallyUnowned")
                    continue;
                
                // These contain: Object, Signals, Fields, Native: {Properties, Methods}
                var result = await template.RenderAsync(new
                {
                    Namespace = CurrentNamespace,
                    Name = cls.ManagedName,
                    Inheritance = _objectService.WriteInheritance(cls),
                    TypeName = cls.CType,
                });

                var path = Path.Combine(dir, $"{cls.ManagedName}.Generated.cs");
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
            foreach (Callback dlg in Namespace.Callbacks)
            {
                var result = await template.RenderAsync(new
                {
                    Namespace = CurrentNamespace,
                    ReturnValue = _uncategorisedService.WriteReturnValue(dlg),
                    WrapperType = dlg.NativeName,
                    WrappedType = dlg.ManagedName,
                    ManagedParameters = _uncategorisedService.WriteParameters(dlg),
                });

                var path = Path.Combine(dir, $"{dlg.ManagedName}.Generated.cs");
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
