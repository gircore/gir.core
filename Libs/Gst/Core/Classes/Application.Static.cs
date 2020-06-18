using System;

namespace Gir.Core.Gst
{
    public class Application
    {
        public static void Init()
        {
            var argc = 0;
            var argv = IntPtr.Zero;

            global::Gst.Methods.init(ref argc, ref argv);
        }
    }
}