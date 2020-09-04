using System;

namespace GLib
{
    public partial class GException :IDisposable
    {
        private bool disposedValue = false;

         ~GException() => Dispose(false);

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if(disposing)
                {
                    Error = null;
                    message = null;
                }

                Free();
                disposedValue = true;
            }
        }
    }
}