using System;
using System.Collections.Generic;
using System.Linq;
using Repository.Analysis;
using Repository.Model;
using Scriban.Runtime;

#nullable enable

namespace Generator.Services.Writer
{
    public interface IWriteSymbolsService
    {
        void WriteSymbols(string projectName, string templateName, string subfolder, string name, IEnumerable<Symbol> symbols, Namespace @namespace);
    }

    public class WriteSymbolsService : IWriteSymbolsService
    {
        private readonly IWriteHelperService _writeHelperService;

        public WriteSymbolsService(IWriteHelperService writeHelperService)
        {
            _writeHelperService = writeHelperService;
        }

        public void WriteSymbols(string projectName, string templateName, string subfolder, string name, IEnumerable<Symbol> symbols, Namespace @namespace)
        {
            var scriptObject = new ScriptObject
            {
                {name.ToLower(), symbols}, 
                {"namespace", @namespace}
            };
            scriptObject.Import("write_native_constant", new Func<Constant, string>(TemplateWriter.WriteNativeConstant));
            scriptObject.Import("write_native_method", new Func<Method, string>(TemplateWriter.WriteNativeMethod));

            try
            {
                _writeHelperService.Write(
                    projectName: projectName,
                    templateName: templateName,
                    folder: subfolder,
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
