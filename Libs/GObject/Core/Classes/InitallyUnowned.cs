using System;

namespace GObject
{
    public class InitallyUnowned : Object
    {
        public InitallyUnowned(IntPtr handle) : base(handle)
        {
            Initialize();
        }

        public InitallyUnowned(params Prop[] properties) : base(properties)
        {
            Initialize();
        }
        
        private void Initialize()
        {
            Sys.Object.ref_sink(Handle);
        }
    }
}