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

            // Update this structure (TODO: Check for NULL)
            this = (TagList)Marshal.PtrToStructure(thisPtr, GetType())!;
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

            // Update this structure (TODO: Check for NULL)
            this = (TagList)Marshal.PtrToStructure(thisPtr, GetType())!;
        }

        public uint GetTagSize(string tag)
        {
            // Marshal this structure
            IntPtr thisPtr = Marshal.AllocHGlobal(Marshal.SizeOf(this));
            Marshal.StructureToPtr(this, thisPtr, false);
            
            // Native Call
            var result = Native.get_tag_size(thisPtr, tag);

            // Update this structure (TODO: Check for NULL)
            this = (TagList)Marshal.PtrToStructure(thisPtr, GetType())!;

            return result;
        }

        public Value GetValueIndex(string tag, uint index)
        {
            // Marshal this structure
            IntPtr thisPtr = Marshal.AllocHGlobal(Marshal.SizeOf(this));
            Marshal.StructureToPtr(this, thisPtr, false);
            
            // Native Call
            var result = Native.get_value_index(thisPtr, tag, index);

            // Update this structure (TODO: Check for NULL)
            this = (TagList)Marshal.PtrToStructure(thisPtr, GetType())!;

            return result;
        }
    }
}
