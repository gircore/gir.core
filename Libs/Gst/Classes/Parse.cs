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
            IntPtr ret = Global.parse_launch(pipelineDescription, out IntPtr error);

            if (error != IntPtr.Zero)
                throw new GLib.GException(error);

            return new Element(ret); //Parse always returns a new object
        }
    }
}
