using System;
using Repository.Model;
using Type = Repository.Model.Type;

namespace Generator
{
    internal static class TypeExtension
    {
        public static string WriteNativeType(this Type type, Namespace currentNamespace)
            => type.Write(Target.Native, currentNamespace);

        public static string WriteManagedType(this Type type, Namespace currentNamespace)
            => type.Write(Target.Managed, currentNamespace);
        
        private static string Write(this Type type, Target target,  Namespace currentNamespace)
        {
            var name = type.SymbolReference.GetSymbol().Write(target, currentNamespace);

            if (type.Array is { })
                name += "[]";

            return name;
        }
    }
}
