using Repository.Analysis;
using Repository.Model;

namespace Generator.Services
{
    public static class ObjectService
    {
        public static string WriteInheritance(Class obj)
        {
            if (obj.Parent is null)
                return string.Empty;

            // Parent Object
            var result = $": {obj.Parent}";

            // Interfaces
            foreach (SymbolReference impl in obj.Implements)
                result += $", {impl}";

            return result;
        }
    }
}
