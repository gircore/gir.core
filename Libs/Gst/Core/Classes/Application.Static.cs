using System;

namespace Gst
{
    public class Application
    {
        public static void Init()
        {
            var argc = 0;
            var argv = IntPtr.Zero;

            Sys.Methods.init(ref argc, ref argv);
        }
    }
}