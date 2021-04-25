using System;
using GObject.Native;

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
            var error = new GLib.Native.Error.Handle(IntPtr.Zero);
            IntPtr ret = Native.Functions.ParseLaunch(pipelineDescription, error);
            GLib.Error.ThrowOnError(error);
            error.Dispose();

            return ObjectWrapper.WrapHandle<Element>(ret, false);
        }

        public static Element BinFromDescription(string binDescription, bool ghostUnlinkedPads)
        {
            var error = new GLib.Native.Error.Handle(IntPtr.Zero);
            IntPtr ret = Native.Functions.ParseBinFromDescription(binDescription, ghostUnlinkedPads, error);
            GLib.Error.ThrowOnError(error);
            error.Dispose();

            return ObjectWrapper.WrapHandle<Element>(ret, false);
        }
    }
}
