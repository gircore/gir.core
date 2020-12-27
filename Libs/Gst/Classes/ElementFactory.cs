namespace Gst
{
    public partial class ElementFactory
    {
        public static Element? Make(string factoryName, string name)
        {
            if (TryWrapHandle(Native.make(factoryName, name), out Element? element))
                return element;

            return null;
        }
    }
}
