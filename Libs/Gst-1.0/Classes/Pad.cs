using System;
using System.Runtime.InteropServices;
using GLib;

namespace Gst
{
    public partial class Pad
    {
        #region Fields

        #endregion

        public static PadLinkReturn Link(Pad sourcePad, Pad sinkPad)
            => Native.Methods.Link(sourcePad.Handle, sinkPad.Handle);

        public static bool Unlink(Pad sourcePad, Pad sinkPad)
            => Native.Methods.Unlink(sourcePad.Handle, sinkPad.Handle);

        public PadLinkReturn Link(Pad sinkPad) => Link(this, sinkPad);
        public bool Unlink(Pad sinkPad) => Unlink(this, sinkPad);

        public bool IsLinked() => Native.Methods.IsLinked(Handle);
        public bool IsBlocked() => Native.Methods.IsBlocked(Handle);
        public bool IsBlocking() => Native.Methods.IsBlocking(Handle);
        public bool IsActive() => Native.Methods.IsActive(Handle);

        public Caps? QueryCaps() => QueryCaps(null);

        public Pad? GetPeer() => WrapNullableHandle<Pad>(Native.Methods.GetPeer(Handle), true);

        public Element? GetParentElement() =>
            WrapNullableHandle<Element>(Native.Methods.GetParentElement(Handle), true);

        public ulong AddProbe(PadProbeType mask, PadProbeCallback callback, DestroyNotify? notify = null)
        {
            // TODO: Can we use null-forgiving here? Fix generator
            return Native.add_probe(Handle, mask, callback, IntPtr.Zero, notify!);
        }

        public Caps? QueryCaps(Caps? filter)
        {
            IntPtr ptr = IntPtr.Zero;
            if (filter != null)
            {
                ptr = Marshal.AllocHGlobal(Marshal.SizeOf(filter));
                Marshal.StructureToPtr(filter, ptr, false);
            }

            IntPtr ret = Native.Methods.QueryCaps(Handle, ptr);

            Marshal.FreeHGlobal(ptr);

            // TODO: Should/can this return null?
            return (Caps?) Marshal.PtrToStructure(ret, typeof(Caps));
        }
    }
}
