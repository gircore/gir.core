using System.Collections.Generic;
using System.Linq;
using System.Text;
using Repository;
using Repository.Analysis;
using Repository.Model;
using Type = Repository.Model.Type;

namespace Generator
{
    internal static class TemplateWriter
    {
        public static string WriteManagedArguments(IEnumerable<Argument> arguments)
        {
            var args = arguments
                .Where(x => x.ClosureIndex == 0) // Exclude "userData" parameters
                .Select(x => WriteManagedSymbolReference(x.SymbolReference) + " " + x.ManagedName);
            return string.Join(", ", args);
        }

        public static string WriteNativeArguments(IEnumerable<Argument> arguments)
        {
            var args = new List<string>();
            foreach (var argument in arguments)
            {
                var builder = new StringBuilder();

                builder.Append(argument.Direction switch
                {
                    Direction.OutCalleeAllocates => "out ",
                    Direction.OutCallerAllocates => "ref ",
                    _ => ""
                });

                builder.Append(WriteNativeSymbolReference(argument.SymbolReference));
                builder.Append(' ');
                builder.Append(argument.ManagedName);

                args.Add(builder.ToString());
            }

            return string.Join(", ", args);
        }

        public static string WriteNativeSymbolReference(SymbolReference symbolReference)
        {
            Symbol symbol = symbolReference.GetSymbol();

            if (symbol is Type)
                return "IntPtr";

            if (symbolReference.IsArray)
                return InternalArray(symbol);

            return InternalType(symbol);
        }

        public static string WriteManagedSymbolReference(SymbolReference symbolReference)
        {
            Symbol symbol = symbolReference.GetSymbol();
            if (symbol is not Type type)
                return symbol.ManagedName;

            return symbolReference switch
            {
                { IsExternal: true, IsArray: true } => ExternalArray(type),
                { IsExternal: true, IsArray: false } => ExternalType(type),
                { IsExternal: false, IsArray: true } => InternalArray(type),
                { IsExternal: false, IsArray: false } => InternalType(type)
            };
        }

        private static string ExternalType(Type type)
            => $"{type.Namespace.Name}.{type.ManagedName}";

        private static string ExternalArray(Type type)
            => $"{type.Namespace.Name}.{type.ManagedName}[]";

        private static string InternalArray(Symbol type)
            => $"{type.ManagedName}[]";

        private static string InternalType(Symbol type)
            => type.ManagedName;

        public static string WriteInheritance(SymbolReference? parent, IEnumerable<SymbolReference> implements)
        {
            var builder = new StringBuilder();

            if (parent is { })
                builder.Append(": " + WriteManagedSymbolReference(parent));

            var refs = implements.ToList();
            if (refs.Count == 0)
                return builder.ToString();

            if (parent is { })
                builder.Append(", ");

            builder.Append(string.Join(", ", refs.Select(WriteManagedSymbolReference)));
            return builder.ToString();
        }

        public static string WriteNativeMethod(Method? method)
        {
            if (method is null )
                return string.Empty;

            var returnValue = WriteNativeSymbolReference(method.ReturnValue.SymbolReference);

            var summaryText = WriteNativeSummary(method);
            var dllImportText = $"[DllImport(\"{method.Namespace.Name}\", EntryPoint = \"{method.NativeName}\")]\r\n";
            var methodText = $"public static extern {returnValue} {method.ManagedName}({WriteNativeArguments(method.Arguments)});\r\n";

            return summaryText + dllImportText + methodText;
        }

        public static string WriteNativeSummary(Method method)
        {
            var builder = new StringBuilder();

            builder.AppendLine($"/// <summary>");
            builder.AppendLine($"/// Calls native method {method.NativeName}.");
            builder.AppendLine($"/// </summary>");

            foreach (var argument in method.Arguments)
            {
                builder.AppendLine($"/// <param name=\"{argument.NativeName}\">Transfer ownership: {argument.Transfer} Nullable: {argument.Nullable}</param>");
            }

            builder.AppendLine($"/// <returns>Transfer ownership: {method.ReturnValue.Transfer} Nullable: {method.ReturnValue.Nullable}</returns>");

            return builder.ToString();
        }

        public static string GetIf(string text, bool condition)
            => condition ? text : "";

        public static string WriteStructFields(IEnumerable<Field> fields)
        {
            var builder = new StringBuilder();

            foreach (Field field in fields)
                builder.AppendLine(WriteStructField(field));

            return builder.ToString();
        }

        private static string WriteStructField(Field field)
        {
            var type = WriteManagedSymbolReference(field.SymbolReference);

            //TODO: The void -> IntPtr conversion is a workaround.
            //There are struct fields which contain a callback instead of a
            //valid symbol reference e.g. fields: io_read, io_write, io_seek, io_close in GLib.
            //We need to support those cases properly to get rid of the workaround.

            // We cannot have "void" in a field declaration, so use IntPtr instead
            if (type == "void")
                type = "IntPtr";

            var builder = new StringBuilder();
            builder.Append(WriteNativeStructFieldSummary(field));
            builder.AppendLine($"public {type} {field.ManagedName};");
            return builder.ToString();
        }

        public static string WriteClassStructFields(IEnumerable<Field> fields, string className)
        {
            var list = fields.ToArray();
            if (list.Length == 0)
                return "";

            var builder = new StringBuilder();
            builder.AppendLine(WriteFirstNativeClassStructField(list[0], className));

            foreach (var field in list[1..])
                builder.AppendLine(WriteStructField(field));

            return builder.ToString();
        }

        public static string WriteClassFields(IEnumerable<Field> fields)
        {
            var list = fields.ToArray();
            if (list.Length == 0)
                return "";

            var builder = new StringBuilder();
            builder.AppendLine(WriteFirstNativeClassField(list[0]));

            foreach (var field in list[1..])
                builder.AppendLine(WriteStructField(field));

            return builder.ToString();
        }

        private static string WriteFirstNativeClassStructField(Field field, string className)
        {
            var builder = new StringBuilder();
            builder.Append(WriteNativeStructFieldSummary(field));
            builder.AppendLine($"    public {className}.Native.ClassStruct {field.ManagedName};");

            return builder.ToString();
        }

        private static string WriteFirstNativeClassField(Field field)
        {
            var builder = new StringBuilder();
            builder.Append(WriteNativeStructFieldSummary(field));
            builder.AppendLine($"    public {WriteManagedSymbolReference(field.SymbolReference)}.Fields {field.ManagedName};");

            return builder.ToString();
        }

        private static string WriteNativeStructFieldSummary(Field field)
        {
            var builder = new StringBuilder();
            builder.AppendLine($"/// <summary>");
            builder.AppendLine($"/// Field name: {field.NativeName}.");
            builder.AppendLine($"/// </summary>");
            return builder.ToString();
        }

        public static string WriteNativeConstant(Constant constant)
        {
            var type = WriteManagedSymbolReference(constant.SymbolReference);

            var value = type switch
            {
                { } t when t.EndsWith("Flags") => $"({t}) {constant.Value}",
                { } t when t == "string" => "\"" + constant.Value + "\"",
                _ => constant.Value
            };

            return $"public static {type} {constant.ManagedName} = {value};\r\n";
        }

        public static string WriteSignalArgsProperties(IEnumerable<Argument> arguments)
        {
            var builder = new StringBuilder();
            var converter = new CaseConverter(); //TODO Make this a service

            var index = 0;
            foreach (var argument in arguments)
            {
                index += 1;
                var type = WriteManagedSymbolReference(argument.SymbolReference);
                var name = converter.ToPascalCase(argument.ManagedName);

                builder.AppendLine($"public {type} {name} => Args[{index}].Extract<{type}>();");
            }

            return builder.ToString();
        }

        public static string WriteCallbackMarshaller(IEnumerable<Argument> arguments, string funcName, bool hasReturnValue)
        {
            var builder = new StringBuilder();
            var args = new List<string>();

            foreach (Argument arg in arguments)
            {
                // Skip 'user_data' parameters (for callbacks, when closure index is not zero)
                if (arg.ClosureIndex != 0)
                    continue;

                var newName = arg.ManagedName + "Parameter";
                builder.AppendLine(WriteMarshalArgumentToManaged(arg, newName));
                args.Add(newName);
            }

            var funcArgs = string.Join(separator: ", ", values: args);
            var funcCall = hasReturnValue
                ? $"var result = {funcName}({funcArgs});"
                : $"{funcName}({funcArgs});";

            builder.Append(funcCall);

            return builder.ToString();
        }

        public static string WriteMarshalArgumentToManaged(Argument arg, string paramName)
        {
            // TODO: We need to support disguised structs (opaque types)
            Symbol symbol = arg.SymbolReference.GetSymbol();
            var fromName = arg.ManagedName;
            var managedType = symbol.ManagedName;

            var expression = symbol switch
            {
                // GObject -> Use Object.WrapHandle
                Class => $"Object.WrapHandle<{managedType}>({fromName});",

                // Struct -> Use struct marshalling (TODO: Should support opaque types)
                Record => $"Marshal.PtrToStructure<{managedType}>({fromName});",

                // Other -> Try a brute-force cast
                _ => $"({managedType}){fromName};"
            };

            return $"{managedType} {paramName} = " + expression;
        }

        public static bool SignalsHaveArgs(IEnumerable<Signal> signals)
            => signals.Any(x => x.Arguments.Any());
    }
}
