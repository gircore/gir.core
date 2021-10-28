using System;
using System.Collections.Generic;
using System.Linq;

namespace Generator3.Renderer
{
    public static class Fields
    {
        public static string Render(this IEnumerable<Model.Field> fields)
        {
            return fields
                .Select(field => field.Render())
                .Join(Environment.NewLine);
        }
    }
}
