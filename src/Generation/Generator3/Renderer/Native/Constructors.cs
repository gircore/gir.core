using System;
using System.Collections.Generic;
using System.Linq;

namespace Generator3.Renderer.Native
{
    public static class Constructors
    {
        public static string Render(this IEnumerable<Model.Native.Constructor> nativeConstructors)
        {
            return nativeConstructors
                .Select(nativeConstructor => nativeConstructor.Render())
                .Join(Environment.NewLine);
        }
    }
}
