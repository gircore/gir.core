using System;

namespace GLib
{
    public partial class VariantType : IDisposable
    {
        private bool disposedValue;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                VariantType.free(handle);
                disposedValue = true;
            }
        }

         ~VariantType() => Dispose(false);

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}