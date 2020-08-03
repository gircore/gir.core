using Gst;

namespace Gst
{
    public class Parse
    {
        public static Element Launch(string pipelineDescription)
        {
            var ret = Sys.Methods.parse_launch(pipelineDescription, out var error);
            return new Element(ret); //Parse always returns a new object
        }
    }
}