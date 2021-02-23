using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
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

        //TODO: Call this method in WriterService
        public void WriteRecords(string projectName, string outputDir, string subfolder, string name, IEnumerable<Record> records, Namespace @namespace)
        {
            foreach (var record in records)
            {
                try
                {
                    //TODO: This code is unfinished
                    var scriptObject =  _scriptObjectFactory.CreateForStructs();
                    scriptObject.Import(record);
                    
                    _writeHelperService.Write(
                        projectName: projectName,
                        outputDir: outputDir,
                        templateName: GetTemplateName(record.Type),
                        folder: subfolder,
                        fileName: record.ManagedName,
                        scriptObject: scriptObject
                    );
                }
                catch (Exception ex)
                {
                    Log.Error($"Could not write symbols for {@namespace.Name} / {name}: {ex.Message}");
                }
            }
        }

        private string GetTemplateName(RecordType recordType) => recordType switch
        {
            RecordType.Value => "struct.sbntxt",
            RecordType.Opaque => "class_struct.sbntxt",
            //RecordType.PrivateClass => throw new Exception($"{nameof(RecordType.PrivateClass)} should be handled by the class. Please open a bug report."),
            //RecordType.PublicClass => throw new Exception($"{nameof(RecordType.PublicClass)} should be handled by the class. Please open a bug report."),
            _ => throw new NotImplementedException("Unsupported record type")
        };
    }
}
