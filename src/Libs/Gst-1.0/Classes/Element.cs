using System;
using System.Runtime.InteropServices;
using GLib;
using GObject.Internal;
using Value = GObject.Value;

namespace Gst
{
    public partial class Element
    {
        #region Fields

        /*public State CurrentState
        {
            get => GetObjectStruct<Fields>().current_state;
            set
            {
                Fields fields = GetObjectStruct<Fields>();
                fields.current_state = value;
                SetObjectStruct(fields);
            }
        }*/

        // NOTE: Be careful about providing access to fields
        // We should (almost) always go through methods, unless
        // we have a good reason. This field accessor below caused
        // severe memory corruption issues when used.

        // public Bus Bus
        // {
        //     get
        //     {
        //         Fields fields = GetObjectStruct<Fields>();
        //         IntPtr bus = fields.bus;
        //         return WrapHandle<Bus>(bus);
        //         // WrapHandle<Bus>(GetObjectStruct<Fields>().bus);   
        //     }
        //     set
        //     {
        //         Fields fields = GetObjectStruct<Fields>();
        //         fields.bus = GetHandle(value);
        //         SetObjectStruct(fields);
        //     }
        // }

        #endregion

        public static Element MakeFromUri(URIType type, string uri, string elementName)
        {
            IntPtr result = Internal.Element.MakeFromUri(type, uri, elementName, out var error);

            Error.ThrowOnError(error);

            return ObjectWrapper.WrapHandle<Element>(result, false);
        }

        /*public Bus? GetBus()
        {
            IntPtr ptr = Native.Element.Instance.Methods.GetBus(Handle);
            return GObject.Native.ObjectWrapper.WrapNullableHandle<Bus>(ptr, true);
        }

        public bool AddPad(Pad pad) => Native.Element.Instance.Methods.AddPad(Handle, pad.Handle);

        public StateChangeReturn SetState(State state)
            => Native.Element.Instance.Methods.SetState(Handle, state);

        public StateChangeReturn GetState(out State state, out State pending, ulong timeout)
        {
            IntPtr statePtr = IntPtr.Zero;
            IntPtr pendingPtr = IntPtr.Zero;
            var result = Native.Element.Instance.Methods.GetState(Handle, out statePtr, out pendingPtr, timeout);

            state = Marshal.PtrToStructure<State>(statePtr);
            pending = Marshal.PtrToStructure<State>(pendingPtr);

            Marshal.FreeHGlobal(statePtr);
            Marshal.FreeHGlobal(pendingPtr);

            return result;
        }

        public bool SeekSimple(Format format, SeekFlags seekFlags, long seekPos)
            => Native.Element.Instance.Methods.SeekSimple(Handle, format, seekFlags, seekPos);

        public bool QueryPosition(Format format, out long cur)
        {
            return Native.Element.Instance.Methods.QueryPosition(Handle, format, out  cur);
        }

        public bool QueryDuration(Format format, out long duration)
        {
            return Native.Element.Instance.Methods.QueryDuration(Handle, format, out duration);
        }

        public Pad? GetStaticPad(string name)
            => throw new NotImplementedException(); //TODO WrapNullableHandle<Pad>(Native.Instance.Methods.GetStaticPad(Handle, name), true);

        public static void Unlink(Element src, Element dest)
            => Native.Element.Instance.Methods.Unlink(src.Handle, dest.Handle);

        public void Unlink(Element dest) => Unlink(this, dest);

        public bool Link(Element dest) => Link(this, dest);

        public static bool Link(Element src, Element dest)
            => Native.Element.Instance.Methods.Link(src.Handle, dest.Handle);

        // FIXME: This function is the culprit for wavparse0 errors
        // TODO: Make this work properly, and additionally clean up
        // the API in the process.
        public static bool Link(params Element[] elements)
        {
            // TODO: Should this return false?
            if (elements.Length < 2)
                return false;

            Element prev = elements[0];
            foreach (var el in elements[1..])
            {
                // TODO: Should we try and keep going?
                if (!Link(prev, el))
                    return false;

                prev = el;
            }

            return true;
        }

        public Pad? GetRequestPad(string name)
            => throw new NotImplementedException(); //TODO WrapNullableHandle<Pad>(Native.Instance.Methods.GetRequestPad(Handle, name), true);

        public bool SyncStateWithParent()
            => Native.Element.Instance.Methods.SyncStateWithParent(Handle);*/

        // Some older mono applications appear to use a
        // string indexer to lookup properties from GLib
        // for GStreamer objects, as we do not know plugin
        // objects at compile time.
        //
        // This is a rudimentary implementation of
        // a property indexer in order to help port over
        // mono/gtk2 applications.
        //
        // TODO: We likely want to move this into GObject in the long term
        // e.g. via custom Property Descriptors
        public object? this[string property]
        {
            get
            {
                try
                {
                    return GetProperty(property).Extract();
                }
                catch (Exception e)
                {
                    throw new Exception($"Property Not Found", e);
                }
            }

            set
            {
                try
                {
                    // We intentionally throw an exception if the type of value cannot be wrapped
                    // TODO: Support boxing arbitrary managed types
                    // TODO: Move this checking code into GObject proper for
                    // safer and more reliable access to properties.
                    Value val;
                    if (value?.GetType().IsAssignableTo(typeof(GObject.Object)) ?? false)
                        val = Value.From((Object) value!);
                    else
                        val = Value.From(value);

                    SetProperty(property, val);
                }
                catch (Exception e)
                {
                    throw new Exception($"Property Not Found", e);
                }
            }
        }
    }
}
