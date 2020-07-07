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
                foreach(var child in children)
                    if(child is IDisposable disposable)
                        disposable.Dispose();

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