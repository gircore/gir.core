using System;

namespace GLib.Core
{
    public partial class GException : Exception, IDisposable
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
                    error = null;
                    message = null;
                }

                Free();
                disposedValue = true;
            }
        }
    }
}