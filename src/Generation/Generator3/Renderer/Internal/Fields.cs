using System;
using System.Collections.Generic;
using System.Linq;

namespace Generator3.Renderer.Internal
{
    public static class Fields
    {
        public static string Render(this IEnumerable<Model.Internal.Field> fields)
        {
            return fields
                .Select(field => field.Render())
                .Join(Environment.NewLine);
        }
    }
}
