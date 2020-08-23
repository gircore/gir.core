using System;
using GObject;

namespace Gdk
{
    public class Snapshot : GObject.Object
    {
        protected internal Snapshot(IntPtr ptr) : base(ptr) {}
        protected internal Snapshot(ConstructProp[] properties) : base(properties) {}   
    }
}