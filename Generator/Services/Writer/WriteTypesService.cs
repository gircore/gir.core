using System;
using System.Collections.Generic;
using Repository.Analysis;
using Repository.Model;
using Scriban.Runtime;

namespace Generator.Services.Writer
{
    internal class WriteTypesService
    {
        private readonly WriteHelperService _writeHelperService;

        public WriteTypesService(WriteHelperService writeHelperService)
        {
            _writeHelperService = writeHelperService;
        }

        public void WriteTypes(string projectName, string outputDir, string templateName, string subfolder, IEnumerable<Symbol> objects)
        {
            foreach (Symbol obj in objects)
            {
                var scriptObject = new ScriptObject();
                scriptObject.Import(obj);
                scriptObject.Import("write_native_arguments", new Func<IEnumerable<Argument>, string>(TemplateWriter.WriteNativeArguments));
                scriptObject.Import("write_managed_arguments", new Func<IEnumerable<Argument>, string>(TemplateWriter.WriteManagedArguments));
                scriptObject.Import("write_native_symbol_reference", new Func<SymbolReference, string>(TemplateWriter.WriteNativeSymbolReference));
                scriptObject.Import("write_managed_symbol_reference", new Func<SymbolReference, string>(TemplateWriter.WriteManagedSymbolReference));
                scriptObject.Import("write_native_method", new Func<Method, string>(TemplateWriter.WriteNativeMethod));
                scriptObject.Import("write_inheritance", new Func<SymbolReference?, IEnumerable<SymbolReference>, string>(TemplateWriter.WriteInheritance));
                scriptObject.Import("write_class_fields", new Func<IEnumerable<Field>, string>(TemplateWriter.WriteClassFields));
                scriptObject.Import("write_struct_fields", new Func<IEnumerable<Field>, string>(TemplateWriter.WriteStructFields));
                scriptObject.Import("get_if", new Func<string, bool, string>(TemplateWriter.GetIf));
                scriptObject.Import("get_signal_data", new Func<Signal, SignalHelper>(s => new SignalHelper(s)));
                scriptObject.Import("write_signal_args_properties", new Func<IEnumerable<Argument>, string>(TemplateWriter.WriteSignalArgsProperties));
                scriptObject.Import("signals_have_args", new Func<IEnumerable<Signal>, bool>(TemplateWriter.SignalsHaveArgs));
                scriptObject.Import("write_marshal_argument_to_managed", new Func<Argument, string, string>(TemplateWriter.WriteMarshalArgumentToManaged));
                scriptObject.Import("write_callback_marshaller", new Func<IEnumerable<Argument>, string, bool, string>(TemplateWriter.WriteCallbackMarshaller));
                scriptObject.Import("write_class_struct_fields", new Func<IEnumerable<Field>, string, string>(TemplateWriter.WriteClassStructFields));
                scriptObject.Import("get_metadata", new Func<string, object?>(key => obj.Metadata[key])); //TODO: Workaround as long as scriban indexer are broken see https://github.com/scriban/scriban/issues/333

                try
                {
                    _writeHelperService.Write(
                        projectName: projectName,
                        outputDir: outputDir,
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
