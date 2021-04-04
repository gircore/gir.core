using System;
using Repository.Model;

namespace Generator
{
    internal static class SymbolExtension
    {
        internal static string Write(this Symbol symbol, Target target,  Namespace currentNamespace)
        {
            var name = symbol.SymbolName;
            if (!symbol.Namespace.IsForeignTo(currentNamespace))
                return name;

            if (symbol.Namespace is null)
                throw new Exception($"Can not write {nameof(Symbol)}, because namespace is missing");

            return symbol.Namespace.GetName(target) + "." + name;
        }
    }
}
