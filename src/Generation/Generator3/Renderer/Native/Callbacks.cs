using System;
using System.Collections.Generic;
using System.Linq;

namespace Generator3.Renderer.Native
{
    public static class Callbacks
    {
        public static string Render(this IEnumerable<Model.Native.Callback> nativeCallbacks)
        {
            return nativeCallbacks
                .Select(nativeCallback => nativeCallback.Render())
                .Join(Environment.NewLine);
        }
    }
}
