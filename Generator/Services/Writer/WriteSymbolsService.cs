using System;
using System.Collections.Generic;
using Generator.Factories;
using Repository.Analysis;
using Repository.Model;
using Scriban.Runtime;

namespace Generator.Services.Writer
{
    internal class WriteSymbolsService
    {
        private readonly WriteHelperService _writeHelperService;
        private readonly ScriptObjectFactory _scriptObjectFactory;

        public WriteSymbolsService(WriteHelperService writeHelperService, ScriptObjectFactory scriptObjectFactory)
        {
            _writeHelperService = writeHelperService;
            _scriptObjectFactory = scriptObjectFactory;
        }

        public void Write(string projectName, string outputDir, string templateName, string subfolder, IEnumerable<Symbol> objects, Namespace @namespace)
        {
            foreach (Symbol obj in objects)
            {
                var scriptObject = _scriptObjectFactory.CreateComplex(@namespace);
                scriptObject.Import(obj);

                //TODO: Workaround as long as scriban indexer are broken see https://github.com/scriban/scriban/issues/333
                scriptObject.Import("get_metadata", new Func<string, object?>(key => obj.Metadata[key]));
                try
                {
                    _writeHelperService.Write(
                        projectName: projectName,
                        outputDir: outputDir,
                        templateName: templateName,
                        folder: subfolder,
                        fileName: obj.SymbolName,
                        scriptObject: scriptObject
                    );
                }
                catch (Exception ex)
                {
                    Log.Error($"Could not create type {obj.SymbolName}: {ex.Message}");
                }
            }
        }
    }
}
