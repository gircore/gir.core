using System;
using GObject.Internal;

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
        public static Element Launch(string pipelineDescription)
        {
            IntPtr ret = Internal.Functions.ParseLaunch(pipelineDescription, out var error);
            GLib.Error.ThrowOnError(error);

            return ObjectWrapper.WrapHandle<Element>(ret, false);
        }

        public static Element BinFromDescription(string binDescription, bool ghostUnlinkedPads)
        {
            IntPtr ret = Internal.Functions.ParseBinFromDescription(binDescription, ghostUnlinkedPads, out var error);
            GLib.Error.ThrowOnError(error);

            return ObjectWrapper.WrapHandle<Element>(ret, false);
        }
    }
}
