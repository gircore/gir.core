using System;
using System.Collections.Generic;
using Generator.Factories;
using Repository.Model;
using Scriban.Runtime;

namespace Generator.Services.Writer
{
    internal class WriteRecordsService
    {
        private readonly WriteHelperService _writeHelperService;
        private readonly ScriptObjectFactory _scriptObjectFactory;

        public WriteRecordsService(WriteHelperService writeHelperService, ScriptObjectFactory scriptObjectFactory)
        {
            _writeHelperService = writeHelperService;
            _scriptObjectFactory = scriptObjectFactory;
        }

        public void Write(string projectName, string outputDir, IEnumerable<Record> records, Namespace @namespace)
        {
            foreach (var record in records)
            {
                try
                {
                    if (record.Type == RecordType.Opaque)
                    {
                        Log.Debug($"Skipping record {record.ManagedName} because of it s type {record.Type}.");
                        continue;
                    }

                    var scriptObject =  _scriptObjectFactory.CreateComplex(@namespace);
                    scriptObject.Import(record);
                    //TODO: Workaround as long as scriban indexer are broken see https://github.com/scriban/scriban/issues/333
                    scriptObject.Import("get_metadata", new Func<string, object?>(key => record.Metadata[key]));

                    
                    _writeHelperService.Write(
                        projectName: projectName,
                        outputDir: outputDir,
                        templateName: GetTemplateName(record.Type),
                        folder: GetSubfolder(record.Type),
                        fileName: record.ManagedName,
                        scriptObject: scriptObject
                    );
                }
                catch (Exception ex)
                {
                    Log.Error($"Could not write record for {record.ManagedName}: {ex.Message}");
                }
            }
        }

        private string GetSubfolder(RecordType recordType) => recordType switch
        {
            RecordType.PrivateClass => "Classes",
            RecordType.PublicClass => "Classes",
            RecordType.Value => "Structs",
            RecordType.Ref => "Classes",
            _ => throw new NotImplementedException("Unsupported record type")
        };

        private string GetTemplateName(RecordType recordType) => recordType switch
        {
            RecordType.PrivateClass => "classstruct.sbntxt",
            RecordType.PublicClass => "classstruct.sbntxt",
            RecordType.Value => "struct.sbntxt",
            RecordType.Ref => "struct_as_class.sbntxt",
            _ => throw new NotImplementedException("Unsupported record type")
        };
    }
}
