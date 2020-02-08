using System;
using System.Runtime.InteropServices;

namespace GLib.Core
{
    public partial class GException : Exception, IDisposable
    {
        private readonly IntPtr errorHandle;

        #region Properties
        protected GLib.Error? error;
        protected GLib.Error Error => error ??= Marshal.PtrToStructure<GLib.Error>(errorHandle);

        private string? message;
        public override string Message =>  message ??= Marshal.PtrToStringAuto(Error.Message);
        #endregion Properties

        public GException(IntPtr error)
        {
            this.errorHandle = error;
        }

        private void Free() => GLib.Error.free(errorHandle);
    }
}