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
                    var scriptObject =  _scriptObjectFactory.CreateComplex(@namespace);
                    scriptObject.Import(record);
                    //TODO: Workaround as long as scriban indexer are broken see https://github.com/scriban/scriban/issues/333
                    scriptObject.Import("get_metadata", new Func<string, object?>(key => record.Metadata[key]));

                    _writeHelperService.Write(
                        projectName: projectName,
                        outputDir: outputDir,
                        templateName: GetTemplateName(record),
                        folder: GetFolder(record),
                        fileName: record.SymbolName,
                        scriptObject: scriptObject
                    );
                }
                catch (Exception ex)
                {
                    Log.Error($"Could not write record for {record.SymbolName}: {ex.Message}");
                }
            }
        }

        private string GetFolder(Record record)
            => record.GLibClassStructFor?.GetSymbol() switch
            {
                Interface => Folder.Interfaces,
                Class => Folder.Classes,
                _ => Folder.Records
            };

        private string GetTemplateName(Record record)
            => record.GLibClassStructFor?.GetSymbol() switch
            {
                Interface => "interfacestruct.sbntxt",
                Class => "classstruct.sbntxt",
                _ => "struct.sbntxt",
            };
    }
}
