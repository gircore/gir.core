using System;
using System.Collections.Generic;
using System.Linq;

namespace Generator3.Renderer
{
    public static class NativeConstructors
    {
        public static string Render(this IEnumerable<Model.NativeConstructor> nativeConstructors)
        {
            return nativeConstructors
                .Select(nativeConstructor => nativeConstructor.Render())
                .Join(Environment.NewLine);
        }
    }
}
