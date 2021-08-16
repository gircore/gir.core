using System;

namespace Gst
{
    public partial class ElementFactory
    {
        public static Element? Make(string factoryName, string name)
            => throw new NotImplementedException(); //TODO WrapNullableHandle<Element>(Native.Methods.Make(factoryName, name), false);
    }
}
