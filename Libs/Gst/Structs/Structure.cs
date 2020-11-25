using System;
using System.Runtime.InteropServices;

namespace Gst
{
    public partial struct Structure
    {
        public string? GetName()
        {
            // Marshal this structure
            IntPtr thisPtr = Marshal.AllocHGlobal(Marshal.SizeOf(this));
            Marshal.StructureToPtr(this, thisPtr, false);
            
            // Native Call
            IntPtr result = Native.get_name(thisPtr);

            // Update this structure (is this necessary?)
            // Marshal.PtrToStructure(thisPtr, this);

            return Marshal.PtrToStringAnsi(result);
        }
        
        public void SetName(string structureName)
        {
            // Marshal this structure
            IntPtr thisPtr = Marshal.AllocHGlobal(Marshal.SizeOf(this));
            Marshal.StructureToPtr(this, thisPtr, false);
            
            // Native Call
            Native.set_name(thisPtr, structureName);

            // Update this structure
            Marshal.PtrToStructure(thisPtr, this);
        }
    }
}
