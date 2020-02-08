using System.Collections.Generic;
using System.Linq;

namespace CWrapper
{
    public static class IEnumerableParametersExtension
    {
        public static string Write(this IEnumerable<Parameter> parameters)
        {
            if(parameters is null || !parameters.Any())
                return "";

            return string.Join(", ", parameters.Select(x => $"{x.Type} {x.Name}"));
        }
    }
}