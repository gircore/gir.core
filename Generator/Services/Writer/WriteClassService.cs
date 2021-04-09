using System;
using System.Collections.Generic;
using Generator.Factories;
using Repository.Model;
using Scriban.Runtime;

namespace Generator.Services.Writer
{
    internal class WriteClassService
    {
        private readonly WriteHelperService _writeHelperService;
        private readonly ScriptObjectFactory _scriptObjectFactory;

        public WriteClassService(WriteHelperService writeHelperService, ScriptObjectFactory scriptObjectFactory)
        {
            _writeHelperService = writeHelperService;
            _scriptObjectFactory = scriptObjectFactory;
        }

        public void Write(string projectName, string outputDir, IEnumerable<Class> classes, Namespace @namespace, WriterOptions options)
        {
            foreach (Class cls in classes)
            {
                var scriptObject = _scriptObjectFactory.CreateComplexForSymbol(@namespace, cls);
                scriptObject.Import(options);
                
                try
                {
                    _writeHelperService.Write(
                        projectName: projectName,
                        outputDir: outputDir,
                        templateName: "class.sbntxt",
                        folder: Folder.Managed.Classes,
                        fileName: cls.SymbolName,
                        scriptObject: scriptObject
                    );

                    if (cls.IsFundamental)
                        continue;

                    _writeHelperService.Write(
                        projectName: projectName,
                        outputDir: outputDir,
                        templateName: "class.native.instance.sbntxt",
                        folder: Folder.Native.Classes,
                        fileName: cls.SymbolName + ".Instance",
                        scriptObject: scriptObject
                    );
                }
                catch (Exception ex)
                {
                    Log.Error($"Could not create type {cls.SymbolName}: {ex.Message}");
                }
            }
        }
    }
}
