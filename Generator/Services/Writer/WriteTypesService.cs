using System;
using System.Collections.Generic;
using Generator.Factories;
using GirLoader.Output.Model;

namespace Generator.Services.Writer
{
    internal class WriteTypesService
    {
        private readonly WriteHelperService _writeHelperService;
        private readonly ScriptObjectFactory _scriptObjectFactory;

        public WriteTypesService(WriteHelperService writeHelperService, ScriptObjectFactory scriptObjectFactory)
        {
            _writeHelperService = writeHelperService;
            _scriptObjectFactory = scriptObjectFactory;
        }

        public void Write(string projectName, string outputDir, string templateName, string subfolder, IEnumerable<ComplexType> types, Namespace @namespace)
        {
            foreach (var type in types)
            {
                var scriptObject = _scriptObjectFactory.CreateComplexForSymbol(@namespace, type);

                try
                {
                    _writeHelperService.Write(
                        projectName: projectName,
                        outputDir: outputDir,
                        templateName: templateName,
                        folder: subfolder,
                        fileName: type.Name,
                        scriptObject: scriptObject
                    );
                }
                catch (Exception ex)
                {
                    Log.Error($"Could not create type {type.Name}: {ex.Message}");
                }
            }
        }
    }
}
