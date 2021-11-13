using System;
using System.Collections.Generic;
using System.Linq;

namespace Generator3.Renderer.Native
{
    public static class Functions
    {
        public static string Render(this IEnumerable<Model.Native.Function> nativeFunctions)
        {
            return nativeFunctions
                .Select(nativeFunction => nativeFunction.Render())
                .Join(Environment.NewLine);
        }
    }
}
