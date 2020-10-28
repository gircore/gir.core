using System;

namespace Gst
{
    public class Parse
    {
        public static Element Launch(string pipelineDescription)
        {
            IntPtr ret = Global.parse_launch(pipelineDescription, out IntPtr error);

            //TODO: Handle error
            
            return new Element(ret); //Parse always returns a new object
        }
    }
}
