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
        private readonly IWriteSymbolsService _writeSymbolsService;

        public WriterService(IWriteTypesService writeTypesService, IWriteDllImportService writeDllImportService, IWriteSymbolsService writeSymbolsService)
        {
            _writeTypesService = writeTypesService;
            _writeDllImportService = writeDllImportService;
            _writeSymbolsService = writeSymbolsService;
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
                templateName: "interface.sbntxt",
                subfolder: "Interfaces",
                objects: loadedProject.Namespace.Interfaces
            );
            
            _writeTypesService.WriteTypes(
                projectName: loadedProject.Name,
                templateName: "enum.sbntxt",
                subfolder: "Enums",
                objects: loadedProject.Namespace.Enumerations
            );

            _writeSymbolsService.WriteSymbols(
                projectName: loadedProject.Name,
                templateName: "constants.sbntxt",
                subfolder: "Classes",
                name: "Constants",
                symbols: loadedProject.Namespace.Constants,
                @namespace: loadedProject.Namespace
            );
            
            _writeSymbolsService.WriteSymbols(
                projectName: loadedProject.Name,
                templateName: "functions.sbntxt",
                subfolder: "Classes",
                name: "Functions",
                symbols: loadedProject.Namespace.Functions,
                @namespace: loadedProject.Namespace
            );
        }
    }
}
