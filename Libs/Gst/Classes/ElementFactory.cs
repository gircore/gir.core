namespace Gst
{
    public partial class ElementFactory
    {
        public static Element? Make(string factoryName, string name)
            => Wrapper.WrapNullableHandle<Element>(Native.make(factoryName, name), false);
    }
}
