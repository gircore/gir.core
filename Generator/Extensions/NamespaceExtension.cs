
using System;
using Repository.Model;

namespace Generator
{
    internal static class NamespaceExtension
    {
        public static bool IsForeignTo(this Namespace? ns, Namespace? other)
            => ns is not null && other != ns;

        public static string GetName(this Namespace ns, Target target)
        {
            return target switch
            {
                Target.Managed => ns.Name,
                Target.Native => ns.NativeName,
                _ => throw new Exception("Unknown target")
            };
        }
    }
}
