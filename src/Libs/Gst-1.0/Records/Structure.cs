using System;
using System.Runtime.InteropServices;

namespace Gst
{
    public partial class Structure
    {
        public string? GetName()
        {
            throw new NotImplementedException(); //TODO

            // Marshal this structure
            /*IntPtr thisPtr = Marshal.AllocHGlobal(Marshal.SizeOf(this));
            Marshal.StructureToPtr(this, thisPtr, false);

            // Do not free result as ownership is not transferred!
            IntPtr result = Native.get_name(thisPtr);

            // TODO: Do we need to update this structure?
            // Probably just switch to using ref structs everywhere
            // so we don't need to worry about it.

            return Marshal.PtrToStringAnsi(result);*/
        }

        public void SetName(string structureName)
        {
            throw new NotImplementedException(); //TODO

            // Marshal this structure
            /*IntPtr thisPtr = Marshal.AllocHGlobal(Marshal.SizeOf(this));
            Marshal.StructureToPtr(this, thisPtr, false);

            Native.set_name(thisPtr, structureName);

            // Update this structure (TODO: Check for NULL)
            this = (Structure) Marshal.PtrToStructure(thisPtr, GetType())!;*/
        }
    }
}
