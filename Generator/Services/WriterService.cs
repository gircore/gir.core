using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;
using Generator.Services;
using Repository;
using Repository.Model;
using Scriban;

#nullable enable

namespace Generator
{
    public interface IWriterService
    {
        Task WriteAsync(ILoadedProject loadedProject);
    }

    public class WriterService : IWriterService
    {
        private readonly ITemplateReaderService _templateReaderService;

        public WriterService(ITemplateReaderService templateReaderService)
        {
            _templateReaderService = templateReaderService;
        }
        public Task WriteAsync(ILoadedProject loadedProject)
        {
            return Task.Run(async () =>
            {
                await WriteTypes(
                    projectName: loadedProject.Name,
                    templateName: "delegate.sbntxt",
                    subfolder: "Delegates",
                    objects: loadedProject.Namespace.Callbacks
                );
            });
        }
        
        private async Task WriteTypes(string projectName, string templateName, string subfolder, IEnumerable<IType> objects)
        {
            var template = _templateReaderService. ReadGenericTemplate(templateName);
            var dir = CreateSubfolder(projectName, subfolder);
            await GenerateTypes(template, dir, objects);
        }

        private static string CreateSubfolder(string projectName, string subfolder)
        {
            var dir = $"output/{projectName}/{subfolder}/";
            Directory.CreateDirectory(dir);

            return dir;
        }

        private static async Task GenerateTypes(Template template, string folder, IEnumerable<IType> objects)
        {
            // Generate a file for each class
            foreach (IType obj in objects)
            {
                var result = await template.RenderAsync(new
                {
                });

                var path = Path.Combine(folder, $"{obj.ManagedName}.Generated.cs");
                await File.WriteAllTextAsync(path, result);
            }
        }

    }
}
