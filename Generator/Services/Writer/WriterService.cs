using System.Threading.Tasks;
using Generator.Services.Writer;
using Repository;

#nullable enable

namespace Generator.Services.Writer
{
    public interface IWriterService
    {
        Task WriteAsync(LoadedProject loadedProject);
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

        public Task WriteAsync(LoadedProject loadedProject)
        {
            return Task.Run(() => Write(loadedProject));
        }

        public void Write(LoadedProject loadedProject)
        {
            if(loadedProject.Namespace.SharedLibrary is null)
                Log.Debug($"Not generating DLL import helper for namespace {loadedProject.Namespace.Name}: It is missing a shared library info.");
            else
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
            
            _writeTypesService.WriteTypes(
                projectName: loadedProject.Name,
                templateName: "struct.sbntxt",
                subfolder: "Structs",
                objects: loadedProject.Namespace.Records
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
