using System;
using System.Collections.Generic;
using Generator.Factories;
using Repository.Model;
using Scriban.Runtime;

namespace Generator.Services.Writer
{
    internal class WriteInterfaceService
    {
        private readonly WriteHelperService _writeHelperService;
        private readonly ScriptObjectFactory _scriptObjectFactory;

        public WriteInterfaceService(WriteHelperService writeHelperService, ScriptObjectFactory scriptObjectFactory)
        {
            _writeHelperService = writeHelperService;
            _scriptObjectFactory = scriptObjectFactory;
        }

        public void Write(string projectName, string outputDir, IEnumerable<Interface> interfaces, Namespace @namespace)
        {
            foreach (var iface in interfaces)
            {
                try
                {
                    var scriptObject =  _scriptObjectFactory.CreateComplexForSymbol(@namespace, iface);

                    _writeHelperService.Write(
                        projectName: projectName,
                        outputDir: outputDir,
                        templateName: "interface.sbntxt",
                        folder: Folder.Managed.Interfaces,
                        fileName: iface.SymbolName ,
                        scriptObject: scriptObject
                    );
                    
                    _writeHelperService.Write(
                        projectName: projectName,
                        outputDir: outputDir,
                        templateName: "native.interface.sbntxt",
                        folder: Folder.Native.Interfaces,
                        fileName: iface.SymbolName,
                        scriptObject: scriptObject
                    );
                }
                catch (Exception ex)
                {
                    Log.Error($"Could not write interface for {iface.SymbolName}: {ex.Message}");
                }
            }
        }
    }
}
