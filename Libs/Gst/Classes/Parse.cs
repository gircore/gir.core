using System;

namespace Gst
{
    public static class Parse
    {
        /// <summary>
        /// Creates a new pipeline.
        /// </summary>
        /// <param name="pipelineDescription">Description of the pipeline</param>
        /// <returns>A new element</returns>
        /// <exception cref="GLib.GException">Throws an exception in case of an error</exception>
        public static Element? Launch(string pipelineDescription)
        {
            IntPtr ret = Global.Native.parse_launch(pipelineDescription, out IntPtr error);

            GLib.Error.HandleError(error);

            return GObject.Object.WrapHandle<Element>(ret);
        }

        public static Element? BinFromDescription(string binDescription, bool ghostUnlinkedPads)
        {
            IntPtr ret = Global.Native.parse_bin_from_description(binDescription, ghostUnlinkedPads, out IntPtr error);
            
            GLib.Error.HandleError(error);
            
            return GObject.Object.TryWrapHandle<Element>(ret, out Element? element) ? element : null;
        }
    }
}
