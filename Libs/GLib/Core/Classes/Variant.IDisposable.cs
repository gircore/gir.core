using System;

namespace GLib
{
    public partial class Variant : IDisposable
    {
        private bool disposedValue;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                foreach(var child in children)
                    if(child is IDisposable disposable)
                        disposable.Dispose();

                Sys.Variant.unref(handle);
                disposedValue = true;
            }
        }

         ~Variant() => Dispose(false);

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}