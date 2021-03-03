using System;
using Repository.Model;

namespace Generator
{
    public static class ReturnValueExtension
    {
        public static string WriteNative(this ReturnValue returnValue, Namespace currentNamespace)
        {
            var symbol = returnValue.SymbolReference.GetSymbol();
            var type = symbol.NativeName;

            if (returnValue.Array is { })
                type = type + "[]";

            if (!symbol.IsForeignTo(currentNamespace))
                return type;

            if (symbol.Namespace is null)
                throw new Exception("Can not write return value, because namespace is missing");

            return symbol.Namespace.Name + "." + type;
        }

        public static string WriteManaged(this ReturnValue returnValue)
        {
            return "ReturnValueManaged";
        }
    }
}
