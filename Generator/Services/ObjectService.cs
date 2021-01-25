using Repository.Analysis;
using Repository.Model;

namespace Generator.Services
{
    public class ObjectService
    {
        public string WriteInheritance(Class obj)
        {
            if (obj.Parent is null)
                return string.Empty;

            // Parent Object
            var result = $": {obj.Parent}";

            // Interfaces
            foreach (TypeReference impl in obj.Implements)
                result += $", {impl}";

            return result;
        }
    }
}
