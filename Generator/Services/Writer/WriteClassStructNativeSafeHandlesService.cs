using System;
using System.Collections.Generic;
using Generator.Factories;
using Repository.Model;
using Scriban.Runtime;

namespace Generator.Services.Writer
{
    internal class WriteClassStructNativeSafeHandlesService
    {
        private readonly WriteHelperService _writeHelperService;
        private readonly ScriptObjectFactory _scriptObjectFactory;

        public WriteClassStructNativeSafeHandlesService(WriteHelperService writeHelperService, ScriptObjectFactory scriptObjectFactory)
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
                    scriptObject.Import("write_release_memory_call", new Func<string>(() => record.WriteReleaseMemoryCall()));
                    
                    _writeHelperService.Write(
                        projectName: projectName,
                        outputDir: outputDir,
                        templateName: GetTemplateName(record),
                        folder: GetFolder(record),
                        fileName: record.SymbolName + ".SafeHandle",
                        scriptObject: scriptObject
                    );
                }
                catch (Exception ex)
                {
                    Log.Error($"Could not write safe handle for record for {record.SymbolName}: {ex.Message}");
                }
            }
        }

        private string GetFolder(Record record)
        {
            return record.GLibClassStructFor?.GetSymbol() switch
            {
                Class => Folder.Classes,
                Interface => Folder.Interfaces,
                _ => throw new NotSupportedException()
            };
        }
        
        private string GetTemplateName(Record record)
        {
            return record.GLibClassStructFor?.GetSymbol() switch
            {
                Class => "classstructsafehandle.sbntxt",
                Interface => "interfacestructsafehandle.sbntxt",
                _ => throw new NotSupportedException()
            };
        }
    }
}
