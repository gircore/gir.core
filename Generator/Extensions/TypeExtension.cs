using System;
using Repository.Model;
using Type = Repository.Model.Type;

namespace Generator
{
    public static class TypeExtension
    {
        public static string WriteNative(this Type type, Namespace currentNamespace)
        {
            var symbol = type.SymbolReference.GetSymbol();
            var nativeName = symbol.NativeName;

            if (type.Array is { })
                nativeName = nativeName + "[]";

            if (!symbol.IsForeignTo(currentNamespace))
                return nativeName;

            if (symbol.Namespace is null)
                throw new Exception("Can not write type value, because namespace is missing");

            return symbol.Namespace.Name + "." + nativeName;
        }
    }
}
