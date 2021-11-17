using System;
using System.Collections.Generic;
using System.Linq;

namespace Generator3.Renderer.Internal
{
    public static class Functions
    {
        public static string Render(this IEnumerable<Model.Internal.Function> functions)
        {
            return functions
                .Select(function => function.Render())
                .Join(Environment.NewLine);
        }
    }
}
