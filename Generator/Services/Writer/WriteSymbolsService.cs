using System;
using System.Collections.Generic;
using Generator.Factories;
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

        public void WriteSymbols(string projectName, string outputDir, string templateName, string subfolder, string name, IEnumerable<Symbol> symbols, Namespace @namespace)
        {
            var scriptObject = _scriptObjectFactory.CreateBase(@namespace);
            scriptObject.Add(name.ToLower(), symbols);
            scriptObject.Add("namespace", @namespace);
            scriptObject.Import("write_managed_constant", new Func<Constant, string>((c) => c.WriteManaged()));

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
                Log.Error($"Could not write symbols for {@namespace.Name} / {name}: {ex.Message}");
            }
        }
    }
}
