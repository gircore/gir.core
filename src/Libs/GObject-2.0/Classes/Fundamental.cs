using System;

namespace GObject
{
    public abstract class Fundamental
    {
        public IntPtr Handle { get; }

        protected Fundamental(IntPtr ptr)
        {
            Handle = ptr;
        }
    }
}
