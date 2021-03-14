using Repository.Model;
using Type = Repository.Model.Type;

namespace Generator
{
    internal static class TypeExtension
    {
        public static string WriteNativeType(this Type type, Namespace currentNamespace)
            => type.WriteType(Target.Native, currentNamespace);

        public static string WriteManagedType(this Type type, Namespace currentNamespace)
            => type.WriteType(Target.Managed, currentNamespace);
        
        internal static string WriteType(this Type type, Target target,  Namespace currentNamespace)
        {
            var symbol = type.SymbolReference.GetSymbol();
            var name = (type, target) switch
            {
                //Arrays of byte can be marshalled automatically, no IntPtr needed
                ({TypeInformation: {Array:{}}}, Target.Native) when symbol.NativeName == "byte" => "byte",
                
                //Use IntPtr for all types where a pointer is expected
                ({TypeInformation: {IsPointer: true}}, Target.Native) => "IntPtr",
                
                _ => type.SymbolReference.GetSymbol().Write(target, currentNamespace)
            };

            if (type.TypeInformation.Array is { })
                name += "[]";

            return name;
        }
    }
}
