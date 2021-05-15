using System;
using System.Collections.Generic;
using Generator.Factories;
using Gir.Model;

namespace Generator.Services.Writer
{
    internal class WriteCallbacksService
    {
        private readonly WriteHelperService _writeHelperService;
        private readonly ScriptObjectFactory _scriptObjectFactory;

        public WriteCallbacksService(WriteHelperService writeHelperService, ScriptObjectFactory scriptObjectFactory)
        {
            _writeHelperService = writeHelperService;
            _scriptObjectFactory = scriptObjectFactory;
        }

        public void Write(string projectName, string outputDir, IEnumerable<Callback> callbacks, Namespace @namespace)
        {
            foreach (var callback in callbacks)
            {
                try
                {
                    var scriptObject = _scriptObjectFactory.CreateComplexForSymbol(@namespace, callback);

                    _writeHelperService.Write(
                        projectName: projectName,
                        outputDir: outputDir,
                        templateName: "delegate.sbntxt",
                        folder: Folder.Managed.Delegates,
                        fileName: callback.SymbolName,
                        scriptObject: scriptObject
                    );

                    _writeHelperService.Write(
                        projectName: projectName,
                        outputDir: outputDir,
                        templateName: "native.delegate.sbntxt",
                        folder: Folder.Native.Delegates,
                        fileName: callback.SymbolName,
                        scriptObject: scriptObject
                    );
                }
                catch (Exception ex)
                {
                    Log.Error($"Could not write callback {callback.SymbolName}: {ex.Message}");
                }
            }
        }
    }
}
