using System;
using System.Collections.Generic;
using Generator.Factories;
using Repository.Model;
using Scriban.Runtime;

namespace Generator.Services.Writer
{
    internal class WriteSafeHandlesService
    {
        private readonly WriteHelperService _writeHelperService;
        private readonly ScriptObjectFactory _scriptObjectFactory;

        public WriteSafeHandlesService(WriteHelperService writeHelperService, ScriptObjectFactory scriptObjectFactory)
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
                    var scriptObject =  _scriptObjectFactory.CreateComplexForSymbol(@namespace, record);
                    scriptObject.Import("write_release_memory_call", new Func<string>(() => record.WriteReleaseMemoryCall()));
                    
                    _writeHelperService.Write(
                        projectName: projectName,
                        outputDir: outputDir,
                        templateName: "native.safehandle.sbntxt",
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
                Class => Folder.Native.Classes,
                Interface => Folder.Native.Interfaces,
                _ => Folder.Native.Records //Regular struct not a class struct
            };
        }
    }
}
