using System;
using System.Collections.Generic;
using System.Linq;

namespace Generator3.Renderer.Public
{
    public static class Properties
    {
        public static string Render(this IEnumerable<Model.Public.Property> properties)
        {
            return properties
                .Select(property => property.Render())
                .Join(Environment.NewLine);
        }
    }
}
