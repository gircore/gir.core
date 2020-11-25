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
        /// <exception cref="GException">Throws an exception in case of an error</exception>
        public static Element Launch(string pipelineDescription)
        {
            //TODO: As this method wrapps a global function it should probably be part of the toolkit layer.
            
            IntPtr ret = Global.Native.parse_launch(pipelineDescription, out IntPtr error);

            if (error != IntPtr.Zero)
                throw new GLib.GException(error);

            return GObject.Object.WrapPointerAs<Element>(ret); //Parse always returns a new object
        }

        public static Element? BinFromDescription(string binDescription, bool ghostUnlinkedPads)
        {
            IntPtr ret = Global.Native.parse_bin_from_description(binDescription, ghostUnlinkedPads, out IntPtr error);
            
            if (error != IntPtr.Zero)
                GLib.Error.HandleError(error);
            
            return GObject.Object.TryWrapPointerAs<Element>(ret, out Element element) ? element : null;
        }
    }
}
