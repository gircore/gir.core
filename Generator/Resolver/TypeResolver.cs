using System;
using Gir;

namespace Generator
{
    internal class MyType
    {
        public int? ArrayLength { get; set;}
        public bool IsArray { get; set; }
        public string Type { get; set; }
        public bool IsPointer { get; set; }
        public bool IsValueType { get; set; }
        public bool IsParameter { get; set; }

        public MyType(string type)
        {
            Type = type;
        }

    }

    public class TypeResolver
    {
        private readonly AliasResolver aliasResolver;

        public TypeResolver(AliasResolver resolver)
        {
            this.aliasResolver = resolver;
        }

        public string Resolve(IType typeInfo) => typeInfo switch
        {
            { Type: { } gtype } => GetTypeName(ConvertGType(gtype, typeInfo is GParameter)),
            { Array: { Length: { } length, Type: { CType: { } } gtype } } => GetTypeName(ResolveArrayType(gtype, typeInfo is GParameter, length)),
            { Array: { } } => "IntPtr",
            _ => throw new NotSupportedException("Type is missing supported Type information")
        };

        private MyType ResolveArrayType(GType arrayType, bool isParameter, int length)
        {
            var type = ConvertGType(arrayType, isParameter);
            type.IsArray = true;

            return type;
        }

        private MyType ConvertGType(GType gtype, bool isParameter)
        {
            if (gtype.CType is null)
                throw new Exception("GType is missing CType");

            var ctype = gtype.CType;

            if (aliasResolver.TryGetForCType(ctype, out var resolvedCType))
                ctype = resolvedCType;

            var result = ResolveCType(ctype);
            result.IsParameter = isParameter;

            if(!result.IsValueType && gtype.Name is {})
                result.Type = gtype.Name;

            return result;
        }

        private string GetTypeName(MyType type)
            => type switch
            {
                { Type: "void", IsPointer: true } => "IntPtr",
                { Type: "byte", IsPointer: true, IsArray: true } => "ref IntPtr", //string array
                { Type: "byte", IsPointer: true, IsParameter: true } => "string",  //string in parameters are marshalled automatically
                { Type: "byte", IsPointer: true, IsParameter: false } => "IntPtr",
                { IsPointer: true, IsValueType: true } => "ref " + type.Type,
                { IsPointer: true, IsValueType: false } => "IntPtr",
                { IsArray: true, IsValueType: true, IsParameter: true, ArrayLength: {} l } =>GetMarshal(l) + type.Type + "[]",
                { IsArray: true, IsValueType: true, ArrayLength: {} } => type.Type + "[]",
                { IsArray: true, IsValueType: true, ArrayLength: null } => "IntPtr",
                _ => type.Type
            };

        private string GetMarshal(int arrayLength)
            => $"[MarshalAs(UnmanagedType.LPArray, SizeParamIndex={arrayLength})]";

        private MyType ResolveCType(string cType)
        {
            var isPointer = cType.EndsWith("*");
            cType = cType.Replace("*", "").Replace("const ", "");

            var result = cType switch
            {
                "void" => ValueType("void"),
                "gboolean" => ValueType("bool"),
                "gfloat" => ValueType("float"),

                //"GCallback" => ReferenceType("Delegate"), // Signature of a callback is determined by the context in which it is used               

                "gconstpointer" => IntPtr(),
                "va_list" => IntPtr(),
                "gpointer" => IntPtr(),
                "GType" => IntPtr(),
                "tm" => IntPtr(),
                var t when t.StartsWith("Atk") => IntPtr(),
                var t when t.StartsWith("Cogl") => IntPtr(),

                "GValue" => Value(),

                "guint16" => UShort(),
                "gushort" => UShort(),

                "gint16" => Short(),
                "gshort" => Short(),

                "gdouble" => Double(),
                "long double" => Double(),

                "int" => Int(),
                "gint" => Int(),
                "gint32" => Int(),

                "guint" => UInt(),
                "guint32" => UInt(),
                "GQuark" => UInt(),
                "gunichar" => UInt(),

                "guchar" => Byte(),
                "gchar" => Byte(),
                "char" => Byte(),
                "guint8" => Byte(),
                "gint8" => Byte(),

                "glong" => Long(),
                "gssize" => Long(),
                "gint64" => Long(),
                "goffset" => Long(),
                "time_t" => Long(),

                "gsize" => ULong(),
                "guint64" => ULong(),
                "gulong" => ULong(),
                "Window" => ULong(),

                _ => ReferenceType(cType)
            };
            result.IsPointer = isPointer;

            return result;
        }

        private MyType String() => ReferenceType("string");
        private MyType IntPtr() => ValueType("IntPtr");
        private MyType Value() => ValueType("GObject.Value");
        private MyType UShort() => ValueType("ushort");
        private MyType Short() => ValueType("short");
        private MyType Double() => ValueType("double");
        private MyType Int() => ValueType("int");
        private MyType UInt() => ValueType("uint");
        private MyType Byte() => ValueType("byte");
        private MyType Long() => ValueType("long");
        private MyType ULong() => ValueType("ulong");

        private MyType ValueType(string str) => new MyType(str){IsValueType = true};
        private MyType ReferenceType(string str) => new MyType(str);
    }
}
