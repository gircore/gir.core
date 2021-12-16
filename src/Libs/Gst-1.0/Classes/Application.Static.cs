using System;

namespace Gst
{
    public class Application
    {
        public static void Init()
        {
            var argc = IntPtr.Zero;
            Internal.Functions.Init(ref argc, new string[0]);
        }
    }
}
