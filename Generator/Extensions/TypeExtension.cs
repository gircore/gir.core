using Repository.Model;
using Type = Repository.Model.Type;

namespace Generator
{
    internal static class TypeExtension
    {
        internal static string WriteType(this Type type, Target target,  Namespace currentNamespace, bool useSafeHandle = true)
        {
            var symbol = type.SymbolReference.GetSymbol();
            var name = (symbol, type, target) switch
            {
                //Arrays of string can be marshalled automatically, no IntPtr needed
                (_, {TypeInformation: {Array:{}}}, Target.Native) when symbol.SymbolName == "string" => "string",
                
                //Arrays of byte can be marshalled automatically, no IntPtr needed
                (_, {TypeInformation: {Array:{}}}, Target.Native) when symbol.SymbolName == "byte" => "byte",

                //Use IntPtr[] for arrays of SafeHandles as those are not supported by the marshaller
                (Record, {TypeInformation: {IsPointer: true, Array:{}}}, Target.Native) => "IntPtr",
                
                //Use original symbol name for records (remapped to SafeHandles)
                (Record r, {TypeInformation: {IsPointer: true}}, Target.Native) when useSafeHandle
                    => WriteType(currentNamespace, r.Namespace, r.GetMetadataString("SafeHandleRefName")),

                //Use IntPtr for all types where a pointer is expected
                (_, {TypeInformation: {IsPointer: true}}, Target.Native) => "IntPtr",
                
                _ => symbol.Write(target, currentNamespace)
            };

            if (type.TypeInformation.Array is { })
                name += "[]";

            return name;
        }

        private static string WriteType(Namespace currentNamespace, Namespace? targetNamespace, string str)
        {
            if (!currentNamespace.IsForeignTo(targetNamespace))
                return str;

            return targetNamespace?.Name + "." + str;
        }
    }
}
