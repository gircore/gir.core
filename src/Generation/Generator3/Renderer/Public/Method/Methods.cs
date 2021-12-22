using System;
using System.Collections.Generic;
using System.Linq;

namespace Generator3.Renderer.Public
{
    public static class Methods
    {
        public static string Render(this IEnumerable<Model.Public.Method> methods)
        {
            return methods
                .Select(method => method.Render())
                .Join(Environment.NewLine);
        }
    }
}
