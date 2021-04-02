using System;

namespace Gst
{
    public class Application
    {
        public static void Init()
        {
            int argc = 0;
            Functions.Native.Init(ref argc, new string[0]);
        }
    }
}
