using System;

namespace GObject
{
    public partial class Closure
    {
        protected bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                Sys.Closure.unref(handle);
                handle = IntPtr.Zero;
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