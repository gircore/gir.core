using System;
using System.Runtime.InteropServices;

namespace Gst
{
    public class MessageTypeMismatchException : Exception
    {
        public MessageTypeMismatchException(MessageType actual, MessageType expected)
            : base($"Expected message type of {expected.ToString()} but received ${actual.ToString()}.")
        {
            
        }
    }
    
    public partial struct Message
    {
        public MessageType Type => type;

        public Structure GetStructure()
        {
            // Marshal this structure
            IntPtr thisPtr = Marshal.AllocHGlobal(Marshal.SizeOf(this));
            Marshal.StructureToPtr(this, thisPtr, false);
            
            // Native Call
            IntPtr ptr = Native.get_structure(thisPtr);

            // Update this structure (is this necessary?)
            Marshal.PtrToStructure(thisPtr, this);

            return Marshal.PtrToStructure<Structure>(ptr);
        }

        public void ParseStateChanged(out State? oldState, out State? newState, out State? pendingState)
        {
            if (type != MessageType.StateChanged)
                throw new MessageTypeMismatchException(type, MessageType.StateChanged);
            
            // Empty pointers
            IntPtr oldStatePtr = default, newStatePtr = default, pendingStatePtr = default;

            // Marshal this structure
            IntPtr thisPtr = Marshal.AllocHGlobal(Marshal.SizeOf(this));
            Marshal.StructureToPtr(this, thisPtr, false);
            
            // Native Call
            Native.parse_state_changed(thisPtr, ref oldStatePtr, ref newStatePtr, ref pendingStatePtr);

            // Update this structure
            Marshal.PtrToStructure(thisPtr, this);

            // Assign out variables
            oldState = oldStatePtr != IntPtr.Zero ? Marshal.PtrToStructure<State>(oldStatePtr) : null;
            newState = newStatePtr != IntPtr.Zero ? Marshal.PtrToStructure<State>(newStatePtr) : null;
            pendingState = pendingStatePtr != IntPtr.Zero ? Marshal.PtrToStructure<State>(pendingStatePtr) : null;
        }

        public void ParseTag(out TagList? tagList)
        {
            if (type != MessageType.Tag)
                throw new MessageTypeMismatchException(type, MessageType.Tag);
            
            // Empty pointers
            IntPtr tagListPtr = default;

            // Marshal this structure
            IntPtr thisPtr = Marshal.AllocHGlobal(Marshal.SizeOf(this));
            Marshal.StructureToPtr(this, thisPtr, false);
            
            // Native Call
            Native.parse_tag(thisPtr, ref tagListPtr);

            // Update this structure
            Marshal.PtrToStructure(thisPtr, this);

            // Assign out variables
            tagList = tagListPtr != IntPtr.Zero ? Marshal.PtrToStructure<TagList>(tagListPtr) : null;
        }

        public Gst.Object Src
        {
            get => GObject.Object.WrapPointerAs<Gst.Object>(src);
            set => src = GObject.Object.GetHandle(value);
        }
    }
}
