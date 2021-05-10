using System;
using System.Collections.Generic;
using Generator.Factories;
using Repository.Analysis;
using Repository.Model;
using Scriban.Runtime;
using Type = Repository.Model.Type;

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

        public void Write(string projectName, string outputDir, string templateName, string subfolder, IEnumerable<Type> objects, Namespace @namespace)
        {
            foreach (Type obj in objects)
            {
                var scriptObject = _scriptObjectFactory.CreateComplexForSymbol(@namespace, obj);

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
