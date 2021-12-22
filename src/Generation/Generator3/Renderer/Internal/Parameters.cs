using System.Collections.Generic;
using System.Linq;

namespace Generator3.Renderer.Internal
{
    public static class Parameters
    {
        public static string Render(this IEnumerable<Model.Internal.Parameter> parameters)
        {
            return parameters
                .Select(parameter => parameter.Render())
                .Join(", ");
        }
    }
}
