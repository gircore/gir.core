using System;

namespace GObject.Core
{
    public partial class GClosure
    {
        protected bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                global::GObject.Closure.unref(handle);
                handle = IntPtr.Zero;
                disposedValue = true;
            }
        }

         ~GClosure() => Dispose(false);

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}