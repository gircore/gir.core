using System;
using System.Runtime.InteropServices;
using GObject;

namespace Gst
{
    public partial struct TagList
    {
        public void Foreach(TagForeachFunc func)
        {
            // Marshal this structure
            IntPtr thisPtr = Marshal.AllocHGlobal(Marshal.SizeOf(this));
            Marshal.StructureToPtr(this, thisPtr, false);
            
            // Native Call
            Native.@foreach(thisPtr, func, IntPtr.Zero);

            // Update this structure
            Marshal.PtrToStructure(thisPtr, this);
        }
        
        public void Add(TagMergeMode mode, string tag, params Value[] values)
        {
            foreach (Value val in values)
            {
                AddValue(mode, tag, val);
            }
            
            // TODO: Free values afterwards?
        }

        public void AddValue(TagMergeMode mode, string tag, Value value)
        {
            // Marshal this structure
            IntPtr thisPtr = Marshal.AllocHGlobal(Marshal.SizeOf(this));
            Marshal.StructureToPtr(this, thisPtr, false);
            
            // Native Call
            Native.add_value(thisPtr, mode, tag, ref value);

            // Update this structure
            Marshal.PtrToStructure(thisPtr, this);
        }
    }
}
