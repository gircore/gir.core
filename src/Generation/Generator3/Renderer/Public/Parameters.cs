using System.Collections.Generic;
using System.Linq;

namespace Generator3.Renderer.Public
{
    public static class Parameters
    {
        public static string Render(this IEnumerable<Model.Public.Parameter> parameters)
        {
            return parameters
                .Select(parameter => parameter.Render())
                .Join(", ");
        }
    }
}
