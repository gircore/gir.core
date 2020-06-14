using System;

namespace GLib.Core
{
    public partial class GVariant : IDisposable
    {
        private bool disposedValue;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                Variant.unref(handle);
                disposedValue = true;
            }
        }

         ~GVariant() => Dispose(false);

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}