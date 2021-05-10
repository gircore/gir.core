using System;
using System.Runtime.InteropServices;
using GObject;

namespace Gst
{
    public partial class TagList
    {
        public void Foreach(TagForeachFunc func)
        {
            throw new NotImplementedException(); //TODO
            // Marshal this structure
            /*IntPtr thisPtr = Marshal.AllocHGlobal(Marshal.SizeOf(this));
            Marshal.StructureToPtr(this, thisPtr, false);

            Native.@foreach(thisPtr, func, IntPtr.Zero);

            // Update this structure (TODO: Check for NULL)
            this = (TagList) Marshal.PtrToStructure(thisPtr, GetType())!;*/
        }

        public void Add(TagMergeMode mode, string tag, params Value[] values)
        {
            foreach (Value val in values)
            {
                AddValue(mode, tag, val);
            }
        }

        public void AddValue(TagMergeMode mode, string tag, Value value)
        {
            throw new NotImplementedException(); //TODO
            // Marshal this structure
            /*IntPtr thisPtr = Marshal.AllocHGlobal(Marshal.SizeOf(this));
            Marshal.StructureToPtr(this, thisPtr, false);

            Native.add_value(thisPtr, mode, tag, ref value);

            // Update this structure (TODO: Check for NULL)
            this = (TagList) Marshal.PtrToStructure(thisPtr, GetType())!;

            // Dispose of Value afterwards
            value.Dispose();*/
        }

        public uint GetTagSize(string tag)
        {
            throw new NotImplementedException(); //TODO
            // Marshal this structure
            /*IntPtr thisPtr = Marshal.AllocHGlobal(Marshal.SizeOf(this));
            Marshal.StructureToPtr(this, thisPtr, false);

            var result = Native.get_tag_size(thisPtr, tag);

            // Update this structure (TODO: Check for NULL)
            this = (TagList) Marshal.PtrToStructure(thisPtr, GetType())!;

            return result;*/
        }

        public Value GetValueIndex(string tag, uint index)
        {
            throw new NotImplementedException(); //TODO
            // Marshal this structure
            /*IntPtr thisPtr = Marshal.AllocHGlobal(Marshal.SizeOf(this));
            Marshal.StructureToPtr(this, thisPtr, false);

            var result = Native.get_value_index(thisPtr, tag, index);

            // Update this structure (TODO: Check for NULL)
            this = (TagList) Marshal.PtrToStructure(thisPtr, GetType())!;

            return result;*/
        }
    }
}
