using System;
using Repository.Model;
using Type = Repository.Model.Type;

namespace Generator
{
    internal static class SymbolExtension
    {
        internal static string Write(this Type type, Target target, Namespace currentNamespace)
        {
            var name = type.SymbolName;
            if (!type.Namespace.IsForeignTo(currentNamespace))
                return name;

            if (type.Namespace is null)
                throw new Exception($"Can not write {nameof(Type)}, because namespace is missing");

            var ns = type switch
            {
                //Enumerations do not have a native representation they always live in the managed namespace
                Enumeration => type.Namespace.Name,

                _ => type.Namespace.GetName(target)
            };

            return ns + "." + name;
        }

        public static string WriteTypeRegistration(this Type type)
        {
            return $"TypeDictionary.Add(typeof({type.SymbolName}), new GObject.Type(Native.{type.SymbolName}.Instance.Methods.GetGType()));\r\n";
        }
    }
}
