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
            => Native.link(GetHandle(sourcePad), GetHandle(sinkPad));
        
        public static bool Unlink(Pad sourcePad, Pad sinkPad)
            => Native.unlink(GetHandle(sourcePad), GetHandle(sinkPad));
        
        public PadLinkReturn Link(Pad sinkPad) => Link(this, sinkPad);
        public bool Unlink(Pad sinkPad) => Unlink(this, sinkPad);

        public bool IsLinked() => Native.is_linked(Handle);
        public bool IsBlocked() => Native.is_blocked(Handle);
        public bool IsBlocking() => Native.is_blocking(Handle);
        public bool IsActive() => Native.is_active(Handle);

        public Caps? QueryCaps() => QueryCaps(null);

        public Pad? GetPeer() => TryWrapPointerAs<Pad>(Native.get_peer(Handle), out Pad pad) ? pad : null;

        public Element? GetParentElement() =>
            TryWrapPointerAs<Element>(Native.get_parent_element(Handle), out Element element) ? element : null;

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

            IntPtr ret = Native.query_caps(Handle, ptr);
            
            Marshal.FreeHGlobal(ptr);
        
            // TODO: Should/can this return null?
            return (Caps?)Marshal.PtrToStructure(ret, typeof(Caps));
        }
    }
}
