using System;

namespace GObject
{
    internal partial class Closure
    {
        protected bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                Sys.Closure.unref(Handle);
                Handle = IntPtr.Zero;
                disposedValue = true;
            }
        }

         ~Closure() => Dispose(false);

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}