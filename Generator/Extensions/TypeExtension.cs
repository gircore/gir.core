using Repository.Model;
using Type = Repository.Model.Type;

namespace Generator
{
    internal static class TypeExtension
    {
        internal static string WriteType(this Type type, Target target,  Namespace currentNamespace)
        {
            var symbol = type.SymbolReference.GetSymbol();
            var name = (type, target) switch
            {
                //Arrays of string can be marshalled automatically, no IntPtr needed
                ({TypeInformation: {Array:{}}}, Target.Native) when symbol.SymbolName == "string" => "string",
                
                //Arrays of byte can be marshalled automatically, no IntPtr needed
                ({TypeInformation: {Array:{}}}, Target.Native) when symbol.SymbolName == "byte" => "byte",

                //Use IntPtr for all types where a pointer is expected
                ({TypeInformation: {IsPointer: true}}, Target.Native) => "IntPtr",
                
                _ => symbol.Write(target, currentNamespace)
            };

            if (type.TypeInformation.Array is { })
                name += "[]";

            return name;
        }
    }
}
