using System;

namespace GObject
{
    public partial struct InterfaceInfo
    {
        public InterfaceInfo(InterfaceInitFunc? initFunc = null, InterfaceFinalizeFunc? finalizeFunc = null)
        {
            this.interface_init = initFunc!;
            this.interface_finalize = finalizeFunc!;
            interface_data = IntPtr.Zero;
        }
    }
}
