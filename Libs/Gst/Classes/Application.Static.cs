using System;

namespace Gst
{
    public class Application
    {
        public static void Init()
        {
            var argc = 0;
            IntPtr argv = IntPtr.Zero;

            Global.Native.init(ref argc, ref argv);
        }
    }
}
