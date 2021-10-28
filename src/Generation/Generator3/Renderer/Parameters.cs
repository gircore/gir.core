using System.Collections.Generic;
using System.Linq;

namespace Generator3.Renderer
{
    public static class Parameters
    {
        public static string Render(this IEnumerable<Model.Parameter> parameters)
        {
            return parameters
                .Select(parameter => parameter.Render())
                .Join(", ");
        }
    }
}
