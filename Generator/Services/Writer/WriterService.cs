using System.Threading.Tasks;
using Repository;

namespace Generator.Services.Writer
{
    internal class WriterService
    {
        private readonly WriteTypesService _writeTypesService;
        private readonly WriteDllImportService _writeDllImportService;
        private readonly WriteSymbolsService _writeSymbolsService;
        private readonly WriteRecordsService _writeRecordsService;

        public WriterService(WriteTypesService writeTypesService, WriteDllImportService writeDllImportService, WriteSymbolsService writeSymbolsService, WriteRecordsService writeRecordsService)
        {
            _writeTypesService = writeTypesService;
            _writeDllImportService = writeDllImportService;
            _writeSymbolsService = writeSymbolsService;
            _writeRecordsService = writeRecordsService;
        }

        public void Write(LoadedProject loadedProject, string outputDir)
        {
            if (loadedProject.Namespace.SharedLibrary is null)
                Log.Debug($"Not generating DLL import helper for namespace {loadedProject.Namespace.Name}: It is missing a shared library info.");
            else
                _writeDllImportService.WriteDllImport(loadedProject, outputDir);

            _writeTypesService.WriteTypes(
                projectName: loadedProject.Name,
                outputDir: outputDir,
                templateName: "delegate.sbntxt",
                subfolder: "Delegates",
                objects: loadedProject.Namespace.Callbacks,
                @namespace: loadedProject.Namespace
            );

            _writeTypesService.WriteTypes(
                projectName: loadedProject.Name,
                outputDir: outputDir,
                templateName: "class.sbntxt",
                subfolder: "Classes",
                objects: loadedProject.Namespace.Classes,
                @namespace: loadedProject.Namespace
            );

            _writeTypesService.WriteTypes(
                projectName: loadedProject.Name,
                outputDir: outputDir,
                templateName: "interface.sbntxt",
                subfolder: "Interfaces",
                objects: loadedProject.Namespace.Interfaces,
                @namespace: loadedProject.Namespace
            );

            _writeTypesService.WriteTypes(
                projectName: loadedProject.Name,
                outputDir: outputDir,
                templateName: "enum.sbntxt",
                subfolder: "Enums",
                objects: loadedProject.Namespace.Enumerations,
                @namespace: loadedProject.Namespace
            );

            _writeTypesService.WriteTypes(
                projectName: loadedProject.Name,
                outputDir: outputDir,
                templateName: "enum.sbntxt",
                subfolder: "Enums",
                objects: loadedProject.Namespace.Bitfields,
                @namespace: loadedProject.Namespace
            );

            _writeRecordsService.WriteRecords(
                projectName: loadedProject.Name,
                outputDir: outputDir,
                records: loadedProject.Namespace.Records,
                @namespace: loadedProject.Namespace
            );

            _writeSymbolsService.WriteSymbols(
                projectName: loadedProject.Name,
                outputDir: outputDir,
                templateName: "constants.sbntxt",
                subfolder: "Classes",
                name: "Constants",
                symbols: loadedProject.Namespace.Constants,
                @namespace: loadedProject.Namespace
            );

            _writeSymbolsService.WriteSymbols(
                projectName: loadedProject.Name,
                outputDir: outputDir,
                templateName: "functions.sbntxt",
                subfolder: "Classes",
                name: "Functions",
                symbols: loadedProject.Namespace.Functions,
                @namespace: loadedProject.Namespace
            );
        }
    }
}
