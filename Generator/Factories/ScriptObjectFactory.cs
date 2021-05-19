using System;
using System.Collections.Generic;
using Generator.Properties;
using Generator.Services.Writer;
using Repository;
using Repository.Model;
using Scriban.Runtime;

namespace Generator.Factories
{
    public class ScriptObjectFactory
    {
        public ScriptObject CreateBase(Namespace currentNamespace)
        {
            var scriptObject = new ScriptObject();
            scriptObject.Import("write_native_arguments", new Func<ParameterList, string>(a => a.WriteNative(currentNamespace)));
            scriptObject.Import("write_native_arguments_no_safehandle", new Func<ParameterList, string>(a => a.WriteNative(currentNamespace, useSafeHandle: false)));
            scriptObject.Import("write_managed_arguments", new Func<ParameterList, string>(a => a.WriteManaged(currentNamespace)));
            scriptObject.Import("write_native_return_value", new Func<ReturnValue, string>(a => a.WriteNative(currentNamespace)));
            scriptObject.Import("write_managed_return_value", new Func<ReturnValue, string>(a => a.WriteManaged(currentNamespace)));
            scriptObject.Import("write_native_method", new Func<Method, string>(m => m.WriteNative(currentNamespace)));
            scriptObject.Import("write_managed_method", new Func<Method, string, string>((m, name) => m.WriteManaged(new SymbolName(name), currentNamespace)));
            scriptObject.Import("get_if", new Func<string, bool, string>(TemplateWriter.GetIf));

            return scriptObject;
        }

        public ScriptObject CreateComplex(Namespace currentNamespace)
        {
            var scriptObject = CreateBase(currentNamespace);
            scriptObject.Import("write_class_inheritance", new Func<TypeReference?, IEnumerable<TypeReference>, string>((s, l) => TemplateWriter.WriteClassInheritance(s, l, currentNamespace)));
            scriptObject.Import("write_iface_inheritance", new Func<IEnumerable<TypeReference>, string>(l => TemplateWriter.WriteInterfaceInheritance(l, currentNamespace)));
            scriptObject.Import("write_native_parent", new Func<TypeReference?, string>(s => TemplateWriter.WriteNativeParent(s, currentNamespace)));
            scriptObject.Import("write_native_fields", new Func<IEnumerable<Field>, string>(f => f.WriteNative(currentNamespace)));
            scriptObject.Import("get_signal_data", new Func<Signal, SignalHelper>(s => new SignalHelper(s)));
            scriptObject.Import("write_signal_args_properties", new Func<ParameterList, string>(a => a.WriteSignalArgsProperties(currentNamespace)));
            scriptObject.Import("write_callback_marshaller_body", new Func<ParameterList, ReturnValue, string>((a, r) => DelegateGenerator.WriteCallbackMarshallerBody(a, r, currentNamespace)));
            scriptObject.Import("write_callback_marshaller_params", new Func<ParameterList, string>(a => DelegateGenerator.WriteCallbackMarshallerParams(a)));
            scriptObject.Import("write_callback_marshaller_return", new Func<ReturnValue, string, string>((r, n) => DelegateGenerator.WriteCallbackMarshallerReturn(r, n, currentNamespace)));
            scriptObject.Import("return_value_is_void", new Func<ReturnValue, bool>(r => r.IsVoid()));
            scriptObject.Import("write_struct_fields", new Func<IEnumerable<Field>, string>(f => f.WriteNative(currentNamespace)));
            scriptObject.Import("write_union_fields", new Func<IEnumerable<Field>, string>(f => f.WriteUnionStructFields(currentNamespace)));
            scriptObject.Import("write_native_field_delegates", new Func<IEnumerable<Field>, string>(f => f.WriteNativeDelegates(currentNamespace)));
            return scriptObject;
        }

        public ScriptObject CreateComplexForSymbol(Namespace currentNamespace, Repository.Model.Type type)
        {
            var scriptObject = CreateComplex(currentNamespace);
            scriptObject.Import(type);
            //TODO: Workaround as long as scriban indexer are broken see https://github.com/scriban/scriban/issues/333
            scriptObject.Import("get_metadata", new Func<string, object?>(key => type.Metadata[key]));
            scriptObject.Import("write_managed_property_descriptor", new Func<Property, string>(p => PropertyGenerator.WriteDescriptor(p, type, currentNamespace)));
            scriptObject.Import("write_managed_property", new Func<Property, string>(p => PropertyGenerator.WriteProperty(p, currentNamespace)));
            return scriptObject;
        }
    }
}
