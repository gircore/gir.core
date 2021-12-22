using System;
using System.Collections.Generic;
using System.Linq;

namespace Generator3.Renderer.Public
{
    public static class Signals
    {
        public static string Render(this IEnumerable<Model.Public.Signal> signals)
        {
            return signals
                .Select(signal => signal.Render())
                .Join(Environment.NewLine);
        }
    }
}
