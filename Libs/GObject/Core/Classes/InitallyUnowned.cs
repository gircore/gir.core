using System;

namespace GObject
{
    public class InitallyUnowned : Object
    {
        public InitallyUnowned(IntPtr handle) : base(handle)
        {
            Initialize();
        }

        public InitallyUnowned(params ConstructProp[] properties) : base(properties)
        {
            Initialize();
        }
        
        protected override void Initialize()
        {
            Sys.Object.ref_sink(Handle);
        }
    }
}