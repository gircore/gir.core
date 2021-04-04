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
        private readonly WriteClassService _writeClassService;
        private readonly WriteUnionsService _writeUnionsService;
        private readonly WriteSafeHandlesService _writeSafeHandlesService;
        private readonly WriteInterfaceService _writeInterfaceService;
        private readonly WriteCallbacksService _writeCallbacksService;

        public WriterService(WriteSymbolsService writeSymbolsService, WriteDllImportService writeDllImportService, WriteElementsService writeElementsService, WriteRecordsService writeRecordsService, WriteStaticService writeStaticService, WriteClassService writeClassService, WriteUnionsService writeUnionsService, WriteSafeHandlesService writeSafeHandlesService, WriteInterfaceService writeInterfaceService, WriteCallbacksService writeCallbacksService)
        {
            _writeSymbolsService = writeSymbolsService;
            _writeDllImportService = writeDllImportService;
            _writeElementsService = writeElementsService;
            _writeRecordsService = writeRecordsService;
            _writeStaticService = writeStaticService;
            _writeClassService = writeClassService;
            _writeUnionsService = writeUnionsService;
            _writeSafeHandlesService = writeSafeHandlesService;
            _writeInterfaceService = writeInterfaceService;
            _writeCallbacksService = writeCallbacksService;
        }

        public void Write(Namespace ns, string outputDir)
        {
            if (ns.SharedLibrary is null)
                Log.Debug($"Not generating DLL import helper for namespace {ns.Name}: It is missing a shared library info.");
            else
                _writeDllImportService.WriteDllImport(ns, outputDir);

            _writeCallbacksService.Write(
                projectName: ns.ToCanonicalName(),
                outputDir: outputDir,
                callbacks: ns.Callbacks,
                @namespace: ns
            );

            _writeClassService.Write(
                projectName: ns.ToCanonicalName(),
                outputDir: outputDir,
                classes: ns.Classes.Where(x => !x.IsFundamental),
                @namespace: ns
            );

            _writeSymbolsService.Write(
                projectName: ns.ToCanonicalName(),
                outputDir: outputDir,
                templateName: "fundamental.class.sbntxt",
                subfolder: Folder.Managed.Classes,
                objects: ns.Classes.Where(x => x.IsFundamental),
                @namespace: ns
            );

            _writeInterfaceService.Write(
                projectName: ns.ToCanonicalName(),
                outputDir: outputDir,
                interfaces: ns.Interfaces,
                @namespace: ns
            );

            _writeSymbolsService.Write(
                projectName: ns.ToCanonicalName(),
                outputDir: outputDir,
                templateName: "enum.sbntxt",
                subfolder: Folder.Managed.Enums,
                objects: ns.Enumerations,
                @namespace: ns
            );

            _writeSymbolsService.Write(
                projectName: ns.ToCanonicalName(),
                outputDir: outputDir,
                templateName: "enum.sbntxt",
                subfolder: Folder.Managed.Enums,
                objects: ns.Bitfields,
                @namespace: ns
            );

            _writeRecordsService.Write(
                projectName: ns.ToCanonicalName(),
                outputDir: outputDir,
                records: ns.Records,
                @namespace: ns
            );

            _writeSafeHandlesService.Write(
                projectName: ns.ToCanonicalName(),
                outputDir: outputDir,
                records: ns.Records,
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
                subfolder: Folder.Managed.Classes,
                name: "Constants",
                elements: ns.Constants,
                @namespace: ns
            );

            _writeElementsService.Write(
                projectName: ns.ToCanonicalName(),
                outputDir: outputDir,
                templateName: "native.functions.sbntxt",
                subfolder: Folder.Native.Classes,
                name: "Functions",
                elements: ns.Functions,
                @namespace: ns
            );

            _writeStaticService.Write(
                projectName: ns.ToCanonicalName(),
                outputDir: outputDir,
                templateName: "native.extensions.sbntxt",
                subfolder: Folder.Native.Classes,
                name: "Extensions",
                @namespace: ns
            );
        }
    }
}
