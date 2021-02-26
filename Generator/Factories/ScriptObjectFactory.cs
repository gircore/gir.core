using System;
using System.Collections.Generic;
using Repository.Analysis;
using Repository.Model;
using Scriban.Runtime;

namespace Generator.Factories
{
    public class ScriptObjectFactory
    {
        public ScriptObject CreateBase()
        {
            var scriptObject = new ScriptObject();
            scriptObject.Import("write_native_arguments", new Func<IEnumerable<Argument>, string>(TemplateWriter.WriteNativeArguments));
            scriptObject.Import("write_managed_arguments", new Func<IEnumerable<Argument>, string>(TemplateWriter.WriteManagedArguments));
            scriptObject.Import("write_native_symbol_reference", new Func<SymbolReference, string>(TemplateWriter.WriteNativeSymbolReference));
            scriptObject.Import("write_managed_symbol_reference", new Func<SymbolReference, string>(TemplateWriter.WriteManagedSymbolReference));
            scriptObject.Import("write_native_method", new Func<Method, string>(TemplateWriter.WriteNativeMethod));
            scriptObject.Import("get_if", new Func<string, bool, string>(TemplateWriter.GetIf));

            return scriptObject;
        }

        public ScriptObject CreateForStructs()
        {
            var scriptObject = CreateBase();
            scriptObject.Import("write_struct_fields", new Func<IEnumerable<Field>, string>(TemplateWriter.WriteStructFields));
            
            return scriptObject;
        }
    }
}
