using System;

namespace GObject
{
    public class InitiallyUnowned : Object
    {
        internal static readonly TypeDescriptor GTypeDescriptor = TypeDescriptor.For("GInitiallyUnowned", Sys.InitiallyUnowned.get_type);

        public InitiallyUnowned(IntPtr handle) : base(handle)
        {
            Initialize();
        }

        public InitiallyUnowned(params ConstructParameter[] properties) : base(properties)
        {
            Initialize();
        }

        protected override void Initialize()
        {
            Sys.Object.ref_sink(Handle);
        }
    }
}
