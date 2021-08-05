using System;
using GirLoader.Output.Model;
using Type = GirLoader.Output.Model.Type;

namespace Generator
{
    internal static class TypeExtension
    {
        internal static string Write(this Type type, Target target, Namespace currentNamespace)
        {
            return type switch
            {
                PrimitiveType => type.Name,

                // Enumerations/Bitfields do not have a native representation they
                // always live in the managed namespace (see method GetName)
                Enumeration e => $"{e.Repository.Namespace.Name}.{e.Name}",
                Bitfield e => $"{e.Repository.Namespace.Name}.{e.Name}",

                ComplexType c when !c.Repository.Namespace.IsForeignTo(currentNamespace) => c.Name,
                ComplexType c => $"{c.Repository.Namespace.GetName(target)}.{c.Name}",

                _ => throw new Exception($"Unknown type {type}.")
            };
        }

        public static string WriteTypeRegistration(this Type type)
        {
            return $"TypeDictionary.Add(typeof({type.Name}), new GObject.Type(Native.{type.Name}.Instance.Methods.GetGType()));\r\n";
        }
    }
}
