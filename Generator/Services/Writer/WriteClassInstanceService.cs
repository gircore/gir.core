using System;
using System.Collections.Generic;
using Generator.Factories;
using Repository.Model;
using Scriban.Runtime;

namespace Generator.Services.Writer
{
    internal class WriteClassInstanceService
    {
        private readonly WriteHelperService _writeHelperService;
        private readonly ScriptObjectFactory _scriptObjectFactory;

        public WriteClassInstanceService(WriteHelperService writeHelperService, ScriptObjectFactory scriptObjectFactory)
        {
            _writeHelperService = writeHelperService;
            _scriptObjectFactory = scriptObjectFactory;
        }
        
        public void Write(string projectName, string outputDir, string templateName, string subfolder, IEnumerable<Class> classes, Namespace @namespace)
        {
            foreach (Class cls in classes)
            {
                var scriptObject = _scriptObjectFactory.CreateComplex(@namespace);
                scriptObject.Import(cls);

                //TODO: Workaround as long as scriban indexer are broken see https://github.com/scriban/scriban/issues/333
                scriptObject.Import("get_metadata", new Func<string, object?>(key => cls.Metadata[key]));
                try
                {
                    _writeHelperService.Write(
                        projectName: projectName,
                        outputDir: outputDir,
                        templateName: templateName,
                        folder: subfolder,
                        fileName: cls.ManagedName + ".Native.Instance",
                        scriptObject: scriptObject
                    );
                }
                catch (Exception ex)
                {
                    Log.Error($"Could not create type {cls.ManagedName}: {ex.Message}");
                }
            }
        }
    }
}
