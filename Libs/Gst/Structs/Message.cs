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
            // TODO: Check for NULL
            this = (Message)Marshal.PtrToStructure(thisPtr, GetType())!;
            
            // Free Memory
            Marshal.FreeHGlobal(thisPtr);

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
            Native.parse_state_changed(thisPtr, out oldStatePtr, out newStatePtr, out pendingStatePtr);

            // Update this structure (TODO: Check for NULL)
            this = (Message)Marshal.PtrToStructure(thisPtr, GetType())!;
            
            // Free Memory
            Marshal.FreeHGlobal(thisPtr);

            // Assign out variables
            oldState = (State)oldStatePtr;// != IntPtr.Zero ? Marshal.PtrToStructure<State>(oldStatePtr) : null;
            newState = (State)newStatePtr;// != IntPtr.Zero ? Marshal.PtrToStructure<State>(newStatePtr) : null;
            pendingState = (State)pendingStatePtr;// != IntPtr.Zero ? Marshal.PtrToStructure<State>(pendingStatePtr) : null;
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
            Native.parse_tag(thisPtr, out tagListPtr);

            // Update this structure (TODO: Check for NULL)
            this = (Message)Marshal.PtrToStructure(thisPtr, GetType())!;
            
            // Free Memory
            Marshal.FreeHGlobal(thisPtr);

            // Assign out variables
            tagList = tagListPtr != IntPtr.Zero ? Marshal.PtrToStructure<TagList>(tagListPtr) : null;
        }
        
        public void ParseBuffering(out int percent)
        {
            if (type != MessageType.Buffering)
                throw new MessageTypeMismatchException(type, MessageType.Buffering);
            
            // Empty pointers
            percent = 0;

            // Marshal this structure
            IntPtr thisPtr = Marshal.AllocHGlobal(Marshal.SizeOf(this));
            Marshal.StructureToPtr(this, thisPtr, false);
            
            // Native Call
            Native.parse_buffering(thisPtr, out percent);

            // Update this structure (TODO: Check for NULL)
            this = (Message)Marshal.PtrToStructure(thisPtr, GetType())!;
            
            // Free Memory
            Marshal.FreeHGlobal(thisPtr);
        }
        
        // FIXME: This entire function is bad -> Causes memory corruption??
        [DllImport("libgstreamer-1.0-0.dll", EntryPoint = "gst_message_parse_error")]
        static extern void gst_message_parse_error (IntPtr msg, out IntPtr err, out IntPtr debug);
        
        public void ParseError(out GLib.Error? error, out string? debug)
        {
            if (type != MessageType.Error)
                throw new MessageTypeMismatchException(type, MessageType.Error);
            
            // Empty pointers

            // Marshal this structure
            IntPtr thisPtr = Marshal.AllocHGlobal(Marshal.SizeOf(this));
            Marshal.StructureToPtr(this, thisPtr, false);

            // Native Call
            Native.parse_error(thisPtr, out IntPtr errPtr, out IntPtr strPtr);
            // gst_message_parse_error(thisPtr, out errPtr, out strPtr);

            // Update structures (TODO: Check for NULL)
            this = (Message) Marshal.PtrToStructure(thisPtr, GetType())!;
            debug = Marshal.PtrToStringAnsi(strPtr);
            error = Marshal.PtrToStructure<GLib.Error>(errPtr);

            // Free Memory
            Marshal.FreeHGlobal(thisPtr);
        }

        public Gst.Object Src
        {
            get => GObject.Object.WrapPointerAs<Gst.Object>(src);
            set => src = GObject.Object.GetHandle(value);
        }
    }
}
