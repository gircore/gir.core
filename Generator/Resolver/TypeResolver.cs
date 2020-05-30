using System;
using Gir;

namespace Generator
{
    internal enum ResolverResult
    {
        Found = 0,
        NotFound = 1,
        NotSupported = 2        
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
            { Type: {} gtype} => ResolveGType(gtype, typeInfo is GParameter),
            { Array: { Length : {} length, Type: {} gtype}} => ResolveArrayType(gtype, typeInfo is GParameter, length),
            _ => throw new NotSupportedException("Type is missing supported Type information")
        };

        private string ResolveArrayType(GType arrayType, bool isParameter, int length)
        {
            var type = ResolveGType(arrayType, isParameter);

            if(type == "string")
            {
                return "ref IntPtr";
            }
            else if(type != "IntPtr")
            {
                if(length > 0)
                {
                    type = type + "[]";

                    if(isParameter)
                        type = GetMarshal(length) + " " + type;
                }
                else
                {
                    return "IntPtr";
                }
            }

            return type;    
        }

        private string ResolveGType(GType gtype, bool isParameter)
        {
            if(gtype.CType is null)
                throw new Exception("GType is missing CType");

            var ctype = gtype.CType;

            if (aliasResolver.TryGetForCType(ctype, out var resolvedCType))
                ctype = resolvedCType;

            (var result, var typeName, var isPrimitive) = ResolveCType(ctype);
            var isPointer = ctype.EndsWith("*");

            return result switch
            {
                ResolverResult.NotFound => gtype.Name ?? throw new Exception($"GType {ctype} is missing a name"),
                ResolverResult.Found => FixTypeName(typeName, isParameter, isPointer, isPrimitive),
                _ => throw new Exception($"{ctype} is not supported")
            };
        }

        private string FixTypeName(string typeName, bool isParameter, bool isPointer, bool isPrimitive) 
            => (typeName, isParameter, isPointer, isPrimitive) switch
        {
            ("string", true, _, _) => typeName, //string stays string for parameter values, they are marshalled automatically
            (_, _, true, true) => "ref " + typeName,
            (_, _, true, false) => "IntPtr",
            _ => typeName
        };

        private string GetMarshal(int arrayLength)
            => $"[MarshalAs(UnmanagedType.LPArray, SizeParamIndex={arrayLength})]";

        private (ResolverResult result, string Type, bool IsPrimitive) ResolveCType(string cType) => cType switch
        {
            "void" => Primitive("void"),
            "gboolean" => Primitive("bool"),
            "gfloat" => Primitive("float"),

            "GCallback" => Complex("Delegate"), // Signature of a callback is determined by the context in which it is used

            "guchar*" => String(),
            "gchar*" => String(),
            "const gchar*" => String(),
            "const char*" => String(),

            "gconstpointer" => IntPtr(),
            "va_list" => IntPtr(),
            "gpointer" => IntPtr(),
            "GType" => IntPtr(),
            "JSCValue*" => IntPtr(),

            "GValue*" => Value(),
            "const GValue*" => Value(),

            "guint16" => UShort(),
            "gushort" => UShort(),

            "gint16" => Short(),
            "gshort" => Short(),

            "gdouble" => Double(),
            "long double" => Double(),

            "gint" => Int(),
            "gint32" => Int(),
            
            "guint" => UInt(),
            "guint32" => UInt(),
            "GQuark" => UInt(),
            "gunichar" =>UInt(),
            "const gunichar*" => UInt(),

            "guint8" => Byte(),
            "gint8" => Byte(),
            "gchar" => Byte(),
            "guchar" => Byte(),

            "glong" => Long(),
            "gssize" => Long(),
            "gint64" => Long(),
            "goffset" => Long(),

            "gsize" => ULong(),
            "guint64" => ULong(),
            "gulong" => ULong(),
            "Window" => ULong(),

            "TokenValue" => NotSupported(cType),
            "IConv" => NotSupported(cType),

            var t when t.StartsWith("Atk") => NotSupported(t),
            var t when t.StartsWith("Cogl") => NotSupported(t),

            _ => NotFound()
        };

        private (ResolverResult reslt, string Type, bool IsPrimitive) String() 
            => Complex("string");
        private (ResolverResult reslt, string Type, bool IsPrimitive) IntPtr() 
            => Complex("IntPtr");
        private (ResolverResult reslt, string Type, bool IsPrimitive) Value()
            => Primitive("GObject.Value");
        private (ResolverResult reslt, string Type, bool IsPrimitive) UShort()
            => Primitive("ushort");
        private (ResolverResult reslt, string Type, bool IsPrimitive) Short()
            => Primitive("short");
        private (ResolverResult reslt, string Type, bool IsPrimitive) Double()
            => Primitive("double");
        private (ResolverResult reslt, string Type, bool IsPrimitive) Int()
            => Primitive("int");
        private (ResolverResult reslt, string Type, bool IsPrimitive) UInt()
            => Primitive("uint");
        private (ResolverResult reslt, string Type, bool IsPrimitive) Byte()
            => Primitive("byte");
        private (ResolverResult reslt, string Type, bool IsPrimitive) Long()
            => Primitive("long");
        private (ResolverResult reslt, string Type, bool IsPrimitive) ULong()
            => Primitive("ulong");

        private (ResolverResult reslt, string Type, bool IsPrimitive) Primitive(string str) 
            => (ResolverResult.Found, str, true);
        private (ResolverResult reslt, string Type, bool IsPrimitive) Complex(string str) 
            => (ResolverResult.Found, str, false);

        private (ResolverResult reslt, string Type, bool IsPrimitive) NotSupported(string str)
            => (ResolverResult.NotSupported, "", false);
        private (ResolverResult reslt, string Type, bool IsPrimitive) NotFound()
            => (ResolverResult.NotFound, "", false);

    }
}
