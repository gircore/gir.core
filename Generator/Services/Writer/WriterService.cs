using System.Threading.Tasks;
using Generator.Services.Writer;
using Repository;

#nullable enable

namespace Generator.Services.Writer
{
    public interface IWriterService
    {
        Task WriteAsync(ILoadedProject loadedProject);
    }

    public class WriterService : IWriterService
    {
        private readonly IWriteTypesService _writeTypesService;
        private readonly IWriteDllImportService _writeDllImportService;

        public WriterService(IWriteTypesService writeTypesService, IWriteDllImportService writeDllImportService)
        {
            _writeTypesService = writeTypesService;
            _writeDllImportService = writeDllImportService;
        }

        public Task WriteAsync(ILoadedProject loadedProject)
        {
            return Task.Run(() => Write(loadedProject));
        }

        public void Write(ILoadedProject loadedProject)
        {
            _writeDllImportService.WriteDllImport(loadedProject);
            
            _writeTypesService.WriteTypes(
                projectName: loadedProject.Name,
                templateName: "delegate.sbntxt",
                subfolder: "Delegates",
                objects: loadedProject.Namespace.Callbacks
            );

            _writeTypesService.WriteTypes(
                projectName: loadedProject.Name,
                templateName: "class.sbntxt",
                subfolder: "Classes",
                objects: loadedProject.Namespace.Classes
            );
            
            _writeTypesService.WriteTypes(
                projectName: loadedProject.Name,
                templateName: "enum.sbntxt",
                subfolder: "Enums",
                objects: loadedProject.Namespace.Enumerations
            );
        }
    }
}
