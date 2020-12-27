using System;
using GObject;

namespace Gst
{
    public partial class GhostPad
    {
        
        // TODO: Replace with constructor once we've found a pattern for
        // implementing constructor logic. Note that gst_ghost_pad_new is
        // not simply a wrapper for g_object_new and has essential logic inside
        // which *must* be run. Therefore our existing constructor design is
        // inadequate.
        public static GhostPad New(string name, Pad target)
            => WrapHandle<GhostPad>(Native.@new(name, GetHandle(target)));

        public bool SetTarget(Pad newTarget)
            => Native.set_target(Handle, GetHandle(newTarget));
    }
}
