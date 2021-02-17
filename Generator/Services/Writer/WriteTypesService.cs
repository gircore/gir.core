using System;
using System.Collections.Generic;
using Repository.Analysis;
using Repository.Model;
using Scriban.Runtime;

#nullable enable

namespace Generator.Services.Writer
{
    public interface IWriteTypesService
    {
        void WriteTypes(string projectName, string templateName, string subfolder, IEnumerable<IType> objects);
    }

    public class WriteTypesService : IWriteTypesService
    {
        private readonly IWriteHelperService _writeHelperService;

        public WriteTypesService(IWriteHelperService writeHelperService)
        {
            _writeHelperService = writeHelperService;
        }

        public void WriteTypes(string projectName, string templateName, string subfolder, IEnumerable<IType> objects)
        {
            foreach (IType obj in objects)
            {
                var scriptObject = new ScriptObject();
                scriptObject.Import(obj);
                scriptObject.Import("write_native_arguments", new Func<IEnumerable<Argument>, string>(TemplateWriter.WriteNativeArguments));
                scriptObject.Import("write_native_symbol_reference", new Func<SymbolReference, string>(TemplateWriter.WriteNativeSymbolReference));
                scriptObject.Import("write_managed_symbol_reference", new Func<SymbolReference, string>(TemplateWriter.WriteManagedSymbolReference));
                scriptObject.Import("write_native_method", new Func<Method, string>(TemplateWriter.WriteNativeMethod));
                scriptObject.Import("write_inheritance", new Func<SymbolReference?, IEnumerable<SymbolReference>, string>(TemplateWriter.WriteInheritance));
                scriptObject.Import("write_class_fields", new Func<IEnumerable<Field>, string>(TemplateWriter.WriteClassFields));
                scriptObject.Import("get_if", new Func<string, bool, string>(TemplateWriter.GetIf));
                scriptObject.Import("get_signal_data", new Func<Signal, SignalHelper>(s => new SignalHelper(s)));
                scriptObject.Import("write_signal_args_properties", new Func<IEnumerable<Argument>, string>(TemplateWriter.WriteSignalArgsProperties));
                scriptObject.Import("signals_have_args", new Func<IEnumerable<Signal>, bool>(TemplateWriter.SignalsHaveArgs));

                try
                {
                    _writeHelperService.Write(
                        projectName: projectName,
                        templateName: templateName,
                        folder: subfolder,
                        fileName: obj.ManagedName,
                        scriptObject: scriptObject
                    );
                }
                catch (Exception ex)
                {
                    Log.Error($"Could not create type {obj.ManagedName}: {ex.Message}");
                }
            }
        }
    }
}
