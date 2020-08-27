using System;

namespace GObject
{
    public class InitiallyUnowned : Object
    {
        protected new static readonly TypeDescriptor GTypeDescriptor = TypeDescriptor.For("GInitiallyUnowned", Sys.InitiallyUnowned.get_type);

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