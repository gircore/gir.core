using System;
using System.Collections.Generic;
using System.Linq;

namespace Generator3.Renderer.Internal
{
    public static class Methods
    {
        public static string Render(this IEnumerable<Model.Internal.Method> methods)
        {
            return methods
                .Select(method => method.Render())
                .Join(Environment.NewLine);
        }
    }
}
