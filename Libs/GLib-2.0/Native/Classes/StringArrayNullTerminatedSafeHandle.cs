using System;
using System.Runtime.InteropServices;

namespace GLib.Native
{
    /*public class StringArrayNullTerminatedSafeHandle : SafeHandle
    {
        private GCHandle _gcHandle;
        private readonly IntPtr[] _data;
        public StringArrayNullTerminatedSafeHandle(string[] array) : base(IntPtr.Zero, true)
        {
            _data = new IntPtr[array.Length + 1];

            for (var i = 0; i < array.Length; i++)
            {
                _data[i] = Marshal.StringToHGlobalAnsi(array[i]);
            }
            _data[array.Length] = IntPtr.Zero;

            _gcHandle = GCHandle.Alloc(_data, GCHandleType.Pinned);
            SetHandle(_gcHandle.AddrOfPinnedObject());
        }

        protected override bool ReleaseHandle()
        {
            //TODO: Verify string array is released properly
            _gcHandle.Free();
            foreach (IntPtr ptr in _data)
            {
                Marshal.FreeHGlobal(ptr);
            }

            return true;
        }

        public override bool IsInvalid => !_gcHandle.IsAllocated;
    }*/
}
