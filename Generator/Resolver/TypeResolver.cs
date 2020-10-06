using System;
using Gir;

namespace Generator
{
    public class ResolvedType
    {
        public string Type { get; }
        public string Attribute { get; }
        public bool IsRef { get; }

        public ResolvedType(string type, bool isRef = false, string attribute = "")
        {
            Type = type;
            Attribute = attribute;
            IsRef = isRef;
        }

        public override string ToString() => GetTypeString();
        
        public string GetTypeString() => Attribute + (IsRef ? "ref " : string.Empty) + Type;
        public string GetFieldString() => Attribute + (IsRef ? "IntPtr" : Type);
    }
    
    internal class MyType
    {
        public string? ArrayLengthParameter { get; set;}
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

        public ResolvedType Resolve(IType typeInfo) => typeInfo switch
        {
            GField f when  f.Callback is { } => new ResolvedType("IntPtr"),
            { Array: { CType:{} n }} when n.EndsWith("**") => new ResolvedType("IntPtr", true),
            { Type: { } gtype } => GetTypeName(ConvertGType(gtype, typeInfo is GParameter)),
            { Array: { Length: { } length, Type: { CType: { } } gtype } } => GetTypeName(ResolveArrayType(gtype, typeInfo is GParameter, length)),
            { Array: { Length: { } length, Type: { Name: "utf8" } name } } => GetTypeName(StringArray(length, typeInfo is GParameter)),
            { Array: { }} => new ResolvedType("IntPtr"),
            _ => throw new NotSupportedException("Type is missing supported Type information")
        };

        private MyType StringArray(string length, bool isParameter) => new MyType("byte")
        {
            IsArray = true, 
            ArrayLengthParameter = length, 
            IsPointer = true, 
            IsValueType = false, 
            IsParameter = isParameter
        };

        public ResolvedType GetTypeString(GType type)
            => GetTypeName(ConvertGType(type, true));

        private MyType ResolveArrayType(GType arrayType, bool isParameter, string? length)
        {
            var type = ConvertGType(arrayType, isParameter);
            type.IsArray = true;
            type.ArrayLengthParameter = length;

            return type;
        }

        private MyType ConvertGType(GType gtype, bool isParameter)
        {
            var ctype = gtype.CType;
            if (ctype is null)
            {
                Console.WriteLine($"GType is missing CType. Assuming {gtype.Name} as CType");
                ctype = gtype.Name ?? throw new Exception($"GType {gtype.Name} is missing CType");
            }

            if (aliasResolver.TryGetForCType(ctype, out var resolvedCType, out var resolvedName))
                ctype = resolvedCType;

            var result = ResolveCType(ctype);
            result.IsParameter = isParameter;

            if(!result.IsValueType && gtype.Name is {})
            {
                result.Type = resolvedName ?? gtype.Name;
            }

            return result;
        }

        private ResolvedType GetTypeName(MyType type)
            => type switch
            {
                { Type: "gpointer" } => new ResolvedType("IntPtr"),
                { IsArray: false, Type: "void", IsPointer: true } => new ResolvedType("IntPtr"),
                { IsArray: false, Type: "byte", IsPointer: true, IsParameter: true } => new ResolvedType("string"),  //string in parameters are marshalled automatically
                { IsArray: false, Type: "byte", IsPointer: true, IsParameter: false } => new ResolvedType("IntPtr"),
                { IsArray: true, Type: "byte", IsPointer: true, IsParameter: true, ArrayLengthParameter: {} l } => new ResolvedType("string[]", attribute: GetMarshal(l)),
                { IsArray: false, IsPointer: true, IsValueType: true } => new ResolvedType(type.Type, true),
                { IsArray: false, IsPointer: true, IsValueType: false } => new ResolvedType("IntPtr"),
                { IsArray: true, Type: "byte", IsPointer: true } => new ResolvedType("IntPtr", true), //string array
                { IsArray: true, IsValueType: false, IsParameter: true, ArrayLengthParameter: {} l } => new ResolvedType("IntPtr[]", attribute: GetMarshal(l)),
                { IsArray: true, IsValueType: true, IsParameter: true, ArrayLengthParameter: {} l } => new ResolvedType(type.Type + "[]", attribute: GetMarshal(l)),
                { IsArray: true, IsValueType: true, ArrayLengthParameter: {} } => new ResolvedType(type.Type + "[]"),
                { IsArray: true, IsValueType: true, ArrayLengthParameter: null } => new ResolvedType("IntPtr"),
                _ => new ResolvedType(type.Type)
            };

        private string GetMarshal(string arrayLength)
            => $"[MarshalAs(UnmanagedType.LPArray, SizeParamIndex={arrayLength})]";

        private MyType ResolveCType(string cType)
        {
            var isPointer = cType.EndsWith("*");
            cType = cType.Replace("*", "").Replace("const ", "").Replace("volatile ", "");

            var result = cType switch
            {
                "void" => ValueType("void"),
                "gboolean" => ValueType("bool"),
                "gfloat" => Float(),
                "float" => Float(),

                //"GCallback" => ReferenceType("Delegate"), // Signature of a callback is determined by the context in which it is used               

                "gconstpointer" => IntPtr(),
                "va_list" => IntPtr(),
                "gpointer" => IntPtr(),
                "GType" => IntPtr(),
                "tm" => IntPtr(),
                var t when t.StartsWith("Atk") => IntPtr(),
                var t when t.StartsWith("Cogl") => IntPtr(),

                "GValue" => Value(),
                //"GError" => Error(),
                //"GVariantType" => VariantType(),

                "guint16" => UShort(),
                "gushort" => UShort(),

                "gint16" => Short(),
                "gshort" => Short(),

                "double" => Double(),
                "gdouble" => Double(),
                "long double" => Double(),

                "cairo_format_t" => Int(),//Workaround
                "int" => Int(),
                "gint" => Int(),
                "gint32" => Int(),
                "pid_t" => Int(),

                "unsigned" => UInt(),//Workaround
                "guint" => UInt(),
                "guint32" => UInt(),
                "GQuark" => UInt(),
                "gunichar" => UInt(),
                "uid_t" => UInt(),

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
        private MyType Float() => ValueType("float");
        private MyType Error() => ValueType("Error");
        private MyType VariantType() => ValueType("VariantType");

        private MyType ValueType(string str) => new MyType(str){IsValueType = true};
        private MyType ReferenceType(string str) => new MyType(str);
    }
}
