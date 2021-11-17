using System;
using System.Collections.Generic;
using System.Linq;

namespace Generator3.Renderer.Internal
{
    public static class Callbacks
    {
        public static string Render(this IEnumerable<Model.Internal.Callback> callbacks)
        {
            return callbacks
                .Select(callback => callback.Render())
                .Join(Environment.NewLine);
        }
    }
}
