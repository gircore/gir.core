using System;
using System.Collections.Generic;
using System.Linq;

namespace Generator3.Renderer.Internal
{
    public static class Constructors
    {
        public static string Render(this IEnumerable<Model.Internal.Constructor> constructors)
        {
            return constructors
                .Select(constructor => constructor.Render())
                .Join(Environment.NewLine);
        }
    }
}
