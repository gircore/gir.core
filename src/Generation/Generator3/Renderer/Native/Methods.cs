using System;
using System.Collections.Generic;
using System.Linq;

namespace Generator3.Renderer.Native
{
    public static class Methods
    {
        public static string Render(this IEnumerable<Model.Native.Method> nativeMethods)
        {
            return nativeMethods
                .Select(nativeMethod => nativeMethod.Render())
                .Join(Environment.NewLine);
        }
    }
}
