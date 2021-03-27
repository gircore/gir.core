using System.Linq;
using System.Threading.Tasks;
using Repository;
using Repository.Model;

namespace Generator.Services.Writer
{
    internal class WriterService
    {
        private readonly WriteSymbolsService _writeSymbolsService;
        private readonly WriteDllImportService _writeDllImportService;
        private readonly WriteElementsService _writeElementsService;
        private readonly WriteRecordsService _writeRecordsService;
        private readonly WriteStaticService _writeStaticService;
        private readonly WriteClassInstanceService _writeClassInstanceService;
        private readonly WriteUnionsService _writeUnionsService;
        private readonly WriteRecordNativeSafeHandlesService _writeRecordNativeSafeHandlesService;
        private readonly WriteClassStructNativeSafeHandlesService _writeClassStructNativeSafeHandlesService;

        public WriterService(WriteSymbolsService writeSymbolsService, WriteDllImportService writeDllImportService, WriteElementsService writeElementsService, WriteRecordsService writeRecordsService, WriteStaticService writeStaticService, WriteClassInstanceService writeClassInstanceService, WriteUnionsService writeUnionsService, WriteRecordNativeSafeHandlesService writeRecordNativeSafeHandlesService, WriteClassStructNativeSafeHandlesService writeClassStructNativeSafeHandlesService)
        {
            _writeSymbolsService = writeSymbolsService;
            _writeDllImportService = writeDllImportService;
            _writeElementsService = writeElementsService;
            _writeRecordsService = writeRecordsService;
            _writeStaticService = writeStaticService;
            _writeClassInstanceService = writeClassInstanceService;
            _writeUnionsService = writeUnionsService;
            _writeRecordNativeSafeHandlesService = writeRecordNativeSafeHandlesService;
            _writeClassStructNativeSafeHandlesService = writeClassStructNativeSafeHandlesService;
        }

        public void Write(Namespace ns, string outputDir)
        {
            if (ns.SharedLibrary is null)
                Log.Debug($"Not generating DLL import helper for namespace {ns.Name}: It is missing a shared library info.");
            else
                _writeDllImportService.WriteDllImport(ns, outputDir);

            _writeSymbolsService.Write(
                projectName: ns.ToCanonicalName(),
                outputDir: outputDir,
                templateName: "delegate.sbntxt",
                subfolder: "Delegates",
                objects: ns.Callbacks,
                @namespace: ns
            );

            _writeSymbolsService.Write(
                projectName: ns.ToCanonicalName(),
                outputDir: outputDir,
                templateName: "class.sbntxt",
                subfolder: Folder.Classes,
                objects: ns.Classes.Where(x => !x.IsFundamental),
                @namespace: ns
            );
            
            _writeClassInstanceService.Write(
                projectName: ns.ToCanonicalName(),
                outputDir: outputDir,
                templateName: "classinstance.sbntxt",
                subfolder: Folder.Classes,
                classes: ns.Classes.Where(x => !x.IsFundamental),
                @namespace: ns
            );
            
            _writeSymbolsService.Write(
                projectName: ns.ToCanonicalName(),
                outputDir: outputDir,
                templateName: "fundamental.class.sbntxt",
                subfolder: Folder.Classes,
                objects: ns.Classes.Where(x => x.IsFundamental),
                @namespace: ns
            );

            _writeSymbolsService.Write(
                projectName: ns.ToCanonicalName(),
                outputDir: outputDir,
                templateName: "interface.sbntxt",
                subfolder: Folder.Interfaces,
                objects: ns.Interfaces,
                @namespace: ns
            );

            _writeSymbolsService.Write(
                projectName: ns.ToCanonicalName(),
                outputDir: outputDir,
                templateName: "enum.sbntxt",
                subfolder: Folder.Enums,
                objects: ns.Enumerations,
                @namespace: ns
            );

            _writeSymbolsService.Write(
                projectName: ns.ToCanonicalName(),
                outputDir: outputDir,
                templateName: "enum.sbntxt",
                subfolder: Folder.Enums,
                objects: ns.Bitfields,
                @namespace: ns
            );

            _writeRecordsService.Write(
                projectName: ns.ToCanonicalName(),
                outputDir: outputDir,
                records: ns.Records,
                @namespace: ns
            );

            _writeRecordNativeSafeHandlesService.Write(
                projectName: ns.ToCanonicalName(),
                outputDir: outputDir,
                records: ns.Records.Where(x => !x.IsClassStruct),
                @namespace: ns
            );
            
            _writeClassStructNativeSafeHandlesService.Write(
                projectName: ns.ToCanonicalName(),
                outputDir: outputDir,
                records: ns.Records.Where(x => x.IsClassStruct),
                @namespace: ns
            );
            
            _writeUnionsService.Write(
                projectName: ns.ToCanonicalName(),
                outputDir: outputDir,
                unions: ns.Unions,
                @namespace: ns
            );

            _writeElementsService.Write(
                projectName: ns.ToCanonicalName(),
                outputDir: outputDir,
                templateName: "constants.sbntxt",
                subfolder: Folder.Classes,
                name: "Constants",
                elements: ns.Constants,
                @namespace: ns
            );

            _writeElementsService.Write(
                projectName: ns.ToCanonicalName(),
                outputDir: outputDir,
                templateName: "functions.sbntxt",
                subfolder: Folder.Classes,
                name: "Functions",
                elements: ns.Functions,
                @namespace: ns
            );
            
            _writeStaticService.Write(
                projectName: ns.ToCanonicalName(),
                outputDir: outputDir,
                templateName: "extensions.sbntxt",
                subfolder: Folder.Classes,
                name: "Extensions",
                @namespace: ns
            );
        }
    }
}
