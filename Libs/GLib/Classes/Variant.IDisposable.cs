using System;

namespace GLib
{
    public partial class Variant : IDisposable
    {
        #region IDisposable Implementation

        private bool _disposedValue;

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                foreach (Variant? child in _children)
                {
                    if (child is IDisposable disposable)
                        disposable.Dispose();
                }

                unref(_handle);
                _disposedValue = true;
            }
        }

        ~Variant() => Dispose(false);

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}