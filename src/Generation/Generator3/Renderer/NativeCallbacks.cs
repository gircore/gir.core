using System;
using System.Collections.Generic;
using System.Linq;

namespace Generator3.Renderer
{
    public static class NativeCallbacks
    {
        public static string Render(this IEnumerable<Model.NativeCallback> nativeCallbacks)
        {
            return nativeCallbacks
                .Select(nativeCallback => nativeCallback.Render())
                .Join(Environment.NewLine);
        }
    }
}
