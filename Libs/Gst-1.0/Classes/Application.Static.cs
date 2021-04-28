using System;

namespace Gst
{
    public class Application
    {
        public static void Init()
        {
            int argc = 0;
            Native.Functions.Init(ref argc, new string[0]);
        }
    }
}
