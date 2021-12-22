using System;
using System.Collections.Generic;
using System.Linq;

namespace Generator3.Renderer.Internal
{
    public static class Callbacks
    {
        public static string RenderWithAttributes(this IEnumerable<Model.Internal.Callback> callbacks)
        {
            return callbacks
                .Select(callback => callback.RenderWithAttributes())
                .Join(Environment.NewLine);
        }
    }
}
