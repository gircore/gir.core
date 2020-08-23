using System;

namespace GObject
{
    public class InitiallyUnowned : Object
    {
        internal static new Sys.Type GetGType() => new Sys.Type(Sys.InitiallyUnowned.get_type());

        public InitiallyUnowned(IntPtr handle) : base(handle)
        {
            Initialize();
        }

        public InitiallyUnowned(params ConstructProp[] properties) : base(properties)
        {
            Initialize();
        }
        
        protected override void Initialize()
        {
            Sys.Object.ref_sink(Handle);
        }
    }
}