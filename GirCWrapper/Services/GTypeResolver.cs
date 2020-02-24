using System;

namespace Gir
{
    public class GTypeResolver : TypeResolver
    {
        private readonly AliasResolver resolver;

        public GTypeResolver(AliasResolver resolver)
        {
            this.resolver = resolver;
        }

        public string GetType(IType type, bool isParameter)
        {
            var ret = type switch
            {
                { Type : {}} => GetReturn(type.Type, isParameter),
                { Array: {Type:{}}} => GetReturn(type.Array.Type, isParameter),
                _ => throw new NotSupportedException("Type is missing supported Type information")
            };

            if(type.Array is {} && ret != "IntPtr")
            {
                if(ret == "string")
                {
                    return "ref IntPtr";
                }
                else
                {
                    ret = ret + "[]"; 

                    if(isParameter)
                        ret = GetMarshal(type.Array) + " " + ret;
                }
            }

            return ret;    
        }

        private string GetMarshal(GArray array) => $"[MarshalAs(UnmanagedType.LPArray, SizeParamIndex={array.Length})]";

        private string GetReturn(GType gtype, bool isParameter)
        {
            if (gtype.Type.In("va_list", "GType", "gpointer", "gconstpointer"))
                return "IntPtr";

            var type = gtype.Name;

            if (resolver != null && gtype.Type is {} && resolver.TryGet(gtype.Type, out string resolvedType))
                type = resolvedType;

            var isPointer = gtype.Type?.EndsWith("*") ?? false;
            bool isPrimitive;
            (type, isPrimitive) = ResolveGType(type);

            if(type == "string" && isParameter)
                return type; //string stays string for parameter values, they are marshalled automatically
            else if(isPointer && isPrimitive)
                return "ref " + type;
            else if(isPointer && !isPrimitive)
                return "IntPtr";
            else
                return type;
        }

        private static (string Type, bool IsPrimitive) ResolveGType(string type) => type switch
        {
            "none" => ("void", true),
            "gboolean" => ("bool", true),
            "gint16" => ("short", true),
            "guint16" => ("ushort", true),
            "gfloat" => ("float", true),
            "utf8" => ("string", false),
            "filename" => ("string", false),
            "Callback" => ("Delegate", false), // Signature of a callback is determined by the context in which it is used

            "Value" => ("GObject.Value", true),

            var t when t.In("gdouble", "long double") => ("double", true),
            var t when t.In("gint","gint32") => ("int", true),
            var t when t.In("guint", "guint32", "GQuark", "gunichar") => ("uint", true),
            var t when t.In("guint8", "gint8", "gchar") => ("byte", true),
            var t when t.In("glong", "gssize", "gint64") => ("long", true),
            var t when t.In("gsize", "guint64", "gulong", "xlib.Window") => ("ulong", true),

            "IConv" => throw new NotSupportedException($"{type} is not supported"),
            "TokenValue" => throw new NotSupportedException($"{type} is not supported"),
            var t when t.StartsWith("Atk.") => throw new NotSupportedException($"{type} is not supported"),

            _ => (type, false)
        };
    }


}
