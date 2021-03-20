
using Repository.Model;

namespace Generator
{
    internal static class NamespaceExtension
    {
        public static bool IsForeignTo(this Namespace? ns, Namespace? other)
            => ns is not null && other != ns;
    }
}
