using System;

namespace GLib
{
    public partial class GException : IDisposable
    {
        #region IDisposable Implementation

        private bool _disposedValue;

        ~GException() => Dispose(false);

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposedValue)
                return;

            Free();
            _disposedValue = true;
        }

        #endregion
    }
}