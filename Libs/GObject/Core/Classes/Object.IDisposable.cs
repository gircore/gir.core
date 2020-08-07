using System;

namespace GObject
{
    public partial class Object : IDisposable
    {
        private bool disposedValue = false;
        protected bool Disposed => disposedValue;

         ~Object() => Dispose(false);

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

                if(Handle != IntPtr.Zero)
                {
                    Sys.Object.unref(Handle);
                    objects.Remove(Handle);
                }

                _handle = IntPtr.Zero;

                //TODO: Findout about closure release
                /*foreach(var closure in closures)
                    closure.Dispose();*/

                closures.Clear();
            }
        }
    }
}