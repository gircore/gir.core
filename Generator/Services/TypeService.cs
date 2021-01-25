using Repository.Analysis;

namespace Generator.Services
{
    public static class TypeService
    {
        public static string PrintTypeIdentifier(TypeReference type)
        {
            // External Type
            if (type.IsForeign)
                return $"{type.Type.Namespace}.{type.Type.ManagedName}";

            // Internal Type
            return type.Type.ManagedName;
        }
    }
}
