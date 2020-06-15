using System;
using Gst;

namespace Gir.Core.Gst
{
    public class Parse
    {
        public static Element Launch(string pipelineDescription)
        {
            var ret = Methods.parse_launch(pipelineDescription, out var error);

            if(error != IntPtr.Zero)
                throw new GLib.Core.GException(error);

            if(GObject.Core.GObject.TryGetObject<Element>(ret, out var obj))
                return obj;
            else
                return new Element(ret);
        }
    }
}