using System;
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
            var name = (type.SymbolReference, target) switch
            {
                ({IsPointer: true}, Target.Native) => "IntPtr",
                _ => type.SymbolReference.GetSymbol().Write(target, currentNamespace)
            };

            if (type.Array is { })
                name += "[]";

            return name;
        }
    }
}
