using System;
using System.Collections.Generic;
using Repository.Analysis;
using Repository.Model;
using Scriban.Runtime;

#nullable enable

namespace Generator.Services.Writer
{
    public interface IWriteSymbolsService
    {
        void WriteSymbols(string projectName, string templateName, string subfolder, string name, IEnumerable<ISymbol> symbols, Namespace @namespace);
    }

    public class WriteSymbolsService : IWriteSymbolsService
    {
        private readonly IWriteHelperService _writeHelperService;

        public WriteSymbolsService(IWriteHelperService writeHelperService)
        {
            _writeHelperService = writeHelperService;
        }

        public void WriteSymbols(string projectName, string templateName, string subfolder, string name, IEnumerable<ISymbol> symbols, Namespace @namespace)
        {
            var scriptObject = new ScriptObject
            {
                {name, symbols}, 
                {"namespace", @namespace}
            };

            _writeHelperService.Write(
                projectName: projectName,
                templateName: templateName,
                folder: subfolder,
                fileName: name,
                scriptObject: scriptObject
            );
        }
    }
}
