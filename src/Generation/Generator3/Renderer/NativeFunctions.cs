using System;
using System.Collections.Generic;
using System.Linq;

namespace Generator3.Renderer
{
    public static class NativeFunctions
    {
        public static string Render(this IEnumerable<Model.NativeFunction> nativeFunctions)
        {
            return nativeFunctions
                .Select(nativeFunction => nativeFunction.Render())
                .Join(Environment.NewLine);
        }
    }
}
