using System;

namespace GObject.Core
{
    public partial class GObject : IDisposable
    {
        private bool disposedValue = false;
        protected bool Disposed => disposedValue;

         ~GObject() => Dispose(false);

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!Disposed)
            {
                disposedValue = true;

                if(handle != IntPtr.Zero)
                    global::GObject.Object.unref(handle);

                handle = IntPtr.Zero;

                //TODO: Findout about closure release
                /*foreach(var closure in closures)
                    closure.Dispose();*/

                closures.Clear();
            }
        }
    }
}