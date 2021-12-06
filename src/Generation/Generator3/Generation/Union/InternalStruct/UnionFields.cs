using System;
using System.Collections.Generic;
using System.Linq;

namespace Generator3.Generation.Union
{
    public static class UnionFields
    {
        public static string Render(this IEnumerable<UnionField> fields)
        {
            return fields
                .Select(field => field.Render())
                .Join(Environment.NewLine);
        }
    }
}
