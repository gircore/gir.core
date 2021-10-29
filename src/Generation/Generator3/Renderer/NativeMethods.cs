using System;
using System.Collections.Generic;
using System.Linq;

namespace Generator3.Renderer
{
    public static class NativeMethods
    {
        public static string Render(this IEnumerable<Model.NativeMethod> nativeMethods)
        {
            return nativeMethods
                .Select(nativeMethod => nativeMethod.Render())
                .Join(Environment.NewLine);
        }
    }
}
