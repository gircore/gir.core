using System;
using System.Runtime.InteropServices;
using GLib;
using GObject;

namespace Gst
{
    public partial class Element
    {
        #region Fields

        public State CurrentState
        {
            get => GetObjectStruct<Fields>().current_state;
            set
            {
                Fields fields = GetObjectStruct<Fields>();
                fields.current_state = value;
                SetObjectStruct(fields);
            }
        }
        
        public Bus Bus
        {
            get => WrapPointerAs<Bus>(GetObjectStruct<Fields>().bus);
            set
            {
                Fields fields = GetObjectStruct<Fields>();
                fields.bus = GetHandle(value);
                SetObjectStruct(fields);
            }
        }

        #endregion

        public static Element? MakeFromUri(URIType type, string uri, string elementName)
        {
            IntPtr result = Native.make_from_uri(type, uri, elementName, out IntPtr errPtr);

            if (errPtr != IntPtr.Zero)
                Error.HandleError(errPtr);

            return TryWrapPointerAs(result, out Element element) ? element : null;
        }
        
        public Bus GetBus()
        {
            IntPtr ret = Native.get_bus(Handle);
            
            if (GetObject(ret, out Bus obj))
                return obj;

            if(ret == IntPtr.Zero)
                throw new Exception("Could not convert pointer to bus");
            
            return new Bus(ret);
        }

        public bool AddPad(Pad pad) => Native.add_pad(Handle, GetHandle(pad));

        public void SetState(State state) 
            => Native.set_state(Handle, state);

        public StateChangeReturn GetState(out State state, out State pending, ulong timeout)
        {
            IntPtr statePtr = IntPtr.Zero;
            IntPtr pendingPtr = IntPtr.Zero;
            var result = Native.get_state(Handle, ref statePtr, ref pendingPtr, timeout);

            state = Marshal.PtrToStructure<State>(statePtr);
            pending = Marshal.PtrToStructure<State>(pendingPtr);
            
            Marshal.FreeHGlobal(statePtr);
            Marshal.FreeHGlobal(pendingPtr);

            return result;
        }

        public bool SeekSimple(Format format, SeekFlags seekFlags, long seekPos)
            => Native.seek_simple(Handle, format, seekFlags, seekPos);
        
        public bool QueryPosition(Format format, out long cur)
        {
            cur = 0;
            return Native.query_position(Handle, format, ref cur);
        }
        
        public bool QueryDuration(Format format, out long duration)
        {
            duration = 0;
            return Native.query_duration(Handle, format, ref duration);
        }

        public Pad GetStaticPad(string name)
            => WrapPointerAs<Pad>(Native.get_static_pad(Handle, name));
        
        public static void Unlink(Element src, Element dest)
            => Native.unlink(GetHandle(src), GetHandle(dest));
        
        public void Unlink(Element dest) => Unlink(this, dest);

        public bool Link(Element dest) => Link(this, dest);

        public static bool Link(Element src, Element dest)
            => Native.link(GetHandle(src), GetHandle(dest));

        public static bool Link(params Element[] elements)
        {
            // TODO: Should this return false?
            if (elements.Length < 2)
                return false;
            
            Element el0 = elements[0];
            foreach (var el in elements[1..])
            {
                // TODO: Should we try and keep going?
                if (!Link(el0, el))
                    return false;
            }

            return true;
        }

        public Pad? GetRequestPad(string name) =>
            TryWrapPointerAs(Native.get_request_pad(Handle, name), out Pad pad) ? pad : null;
        
        // Banshee appears to use a string indexer to
        // lookup properties from GLib for GStreamer
        // objects, as we do not know plugin information
        // at compile time.
        //
        // FIXME: This is a very rudimentary implementation of
        // a property (?) indexer in order to help port over
        // mono/gtk2 applications. Do we want to keep this
        // in the long term?
        //
        // This is written purely from guesswork as to what
        // the original code does. It might not actually work. Try
        // and fix this ASAP.
        [Obsolete]
        public object? this[string property]
        {
            get
            {
                try
                {
                    return GStreamerGlueGetProperty(property).Extract();
                }
                catch (Exception e)
                {
                    throw new PropertyNotFoundException(e);
                }
            }

            set
            {
                try
                {
                    // We intentionally throw an exception if the type of value cannot be wrapped
                    // TODO: Support boxing arbitrary managed types
                    var val = Value.From(value);
                    GStreamerGlueSetProperty(property, val);
                }
                catch (Exception e)
                {
                    throw new PropertyNotFoundException(e);
                }
            }
        }
    }
}
