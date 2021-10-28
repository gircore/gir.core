using System;
using System.Collections.Generic;
using System.Linq;

namespace Generator3.Renderer
{
    public static class Constants
    {
        public static string Render(this IEnumerable<Model.Constant> constants)
        {
            return constants
                .Select(constant => constant.Render())
                .Join(Environment.NewLine);
        }
    }
}
