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
            var symbol = type.SymbolReference.GetSymbol();
            var name = GetName(symbol, target);

            if (type.Array is { })
                name = name + "[]";

            if (!symbol.IsForeignTo(currentNamespace))
                return name;

            if (symbol.Namespace is null)
                throw new Exception("Can not write type value, because namespace is missing");

            return symbol.Namespace.Name + "." + name;
        }
        
        private static string GetName(Symbol symbol, Target target) => target switch
        {
            Target.Managed => symbol.ManagedName,
            Target.Native => symbol.NativeName,
            _ => throw new Exception($"Unknown {nameof(Target)}")
        };
    }
}
