using System.Collections.Generic;
using System.Linq;
using System.Text;
using Repository.Model;

namespace Generator
{
    public static class FieldsExtension
    {
        public static string WriteNative(this IEnumerable<Field> fields)
        {
            var builder = new StringBuilder();

            foreach (Field field in fields)
                builder.AppendLine(field.WriteNative());

            return builder.ToString();
        }
        
        public static string WriteClassFields(this IEnumerable<Field> fields)
        {
            var list = fields.ToArray();
            if (list.Length == 0)
                return "";

            var builder = new StringBuilder();
            builder.AppendLine(WriteFirstNativeClassField(list[0]));

            foreach (var field in list[1..])
                builder.AppendLine(field.WriteNative());

            return builder.ToString();
        }
        
        private static string WriteFirstNativeClassField(Field field)
        {
            var builder = new StringBuilder();
            builder.Append(field.WriteNativeSummary());
            builder.AppendLine($"    public {field.WriteNative()}.Fields {field.ManagedName};");

            return builder.ToString();
        }
        
        public static string WriteClassStructFields(this IEnumerable<Field> fields, string className)
        {
            var list = fields.ToArray();
            if (list.Length == 0)
                return "";

            var builder = new StringBuilder();
            builder.AppendLine(WriteFirstNativeClassStructField(list[0], className));

            foreach (var field in list[1..])
                builder.AppendLine(field.WriteNative());

            return builder.ToString();
        }

        private static string WriteFirstNativeClassStructField(Field field, string className)
        {
            var builder = new StringBuilder();
            builder.Append(field.WriteNativeSummary());
            builder.AppendLine($"    public {className}.Native.ClassStruct {field.ManagedName};");

            return builder.ToString();
        }
    }
}
