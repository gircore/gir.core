using System;
using System.Collections.Generic;
using Generator.Services.Writer;
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
            scriptObject.Import("write_native_arguments", new Func<IEnumerable<Argument>, string>(a => a.WriteNative()));
            scriptObject.Import("write_managed_arguments", new Func<IEnumerable<Argument>, string>(a => a.WriteManaged()));
            scriptObject.Import("write_native_return_value", new Func<ReturnValue, Namespace, string>((a,n) => a.WriteNative(n)));
            scriptObject.Import("write_managed_return_value", new Func<ReturnValue, string>(a => a.WriteManaged()));
            scriptObject.Import("write_native_method", new Func<Method, Namespace, string>((m,n) => m.WriteNative(n)));
            scriptObject.Import("get_if", new Func<string, bool, string>(TemplateWriter.GetIf));

            return scriptObject;
        }

        public ScriptObject CreateForStructs()
        {
            var scriptObject = CreateBase();
            scriptObject.Import("write_struct_fields", new Func<IEnumerable<Field>, string>(f => f.WriteNative()));
            
            return scriptObject;
        }

        public ScriptObject CreateForClasses()
        {

            var scriptObject = CreateBase();
            scriptObject.Import("write_inheritance", new Func<SymbolReference?, IEnumerable<SymbolReference>, string>(TemplateWriter.WriteInheritance));
            scriptObject.Import("write_class_fields", new Func<IEnumerable<Field>, string>(f => f.WriteClassFields()));
            scriptObject.Import("get_signal_data", new Func<Signal, SignalHelper>(s => new SignalHelper(s)));
            scriptObject.Import("write_signal_args_properties", new Func<IEnumerable<Argument>, string>(a => a.WriteManaged()));
            scriptObject.Import("signals_have_args", new Func<IEnumerable<Signal>, bool>(TemplateWriter.SignalsHaveArgs));
            
            scriptObject.Import("write_marshal_argument_to_managed", new Func<Argument, string, string>(TemplateWriter.WriteMarshalArgumentToManaged));
            scriptObject.Import("write_callback_marshaller", new Func<IEnumerable<Argument>, string, bool, string>(TemplateWriter.WriteCallbackMarshaller));
            scriptObject.Import("write_class_struct_fields", new Func<IEnumerable<Field>, string, string>((f,s) => f.WriteClassStructFields(s)));
            
            return scriptObject;
        }
    }
}
