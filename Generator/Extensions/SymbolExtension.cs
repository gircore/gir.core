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

            var ns = symbol switch
            {
                //Enumerations do not have a native representation they always live in the managed namespace
                Enumeration => symbol.Namespace.Name,
                
                _ => symbol.Namespace.GetName(target)
            };
            
            return ns + "." + name;
        }

        public static string WriteTypeRegistration(this Symbol symbol)
        {
            return $"TypeDictionary.Add(typeof({symbol.SymbolName}), GObject.TypeDescriptor.For(\"{symbol.CTypeName}\", Native.{symbol.SymbolName}.Instance.Methods.GetGType));\r\n";
        }
    }
}
