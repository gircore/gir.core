using System;
using System.Collections.Generic;
using Generator.Factories;
using Repository.Model;
using Scriban.Runtime;

namespace Generator.Services.Writer
{
    internal class WriteStaticService
    {
        private readonly WriteHelperService _writeHelperService;
        private readonly ScriptObjectFactory _scriptObjectFactory;

        public WriteStaticService(WriteHelperService writeHelperService, ScriptObjectFactory scriptObjectFactory)
        {
            _writeHelperService = writeHelperService;
            _scriptObjectFactory = scriptObjectFactory;
        }

        public void Write(string projectName, string outputDir, string templateName, string subfolder, string name, Namespace @namespace)
        {
            var scriptObject = _scriptObjectFactory.CreateBase(@namespace);
            scriptObject.Add("namespace", @namespace);

            try
            {
                _writeHelperService.Write(
                    projectName: projectName,
                    templateName: templateName,
                    folder: subfolder,
                    outputDir: outputDir,
                    fileName: name,
                    scriptObject: scriptObject
                );
            }
            catch (Exception ex)
            {
                Log.Error($"Could not write template for {@namespace.Name} / {name}: {ex.Message}");
            }
        }
    }
}
