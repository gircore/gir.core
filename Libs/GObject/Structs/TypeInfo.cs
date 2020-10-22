using System;

#nullable enable

namespace GObject
{
    public partial struct TypeInfo
    {
        public TypeInfo(ushort class_size, ushort instance_size,
            BaseInitFunc? base_init = null,
            BaseFinalizeFunc? base_finalize = null,
            ClassInitFunc? class_init = null,
            ClassFinalizeFunc? class_finalize = null,
            InstanceInitFunc? instance_init = null)
        {
            // Required Parameters
            this.class_size = class_size;
            this.instance_size = instance_size;

            // Class Functions
            this.base_init = base_init!;
            this.base_finalize = base_finalize!;
            this.class_init = class_init!;
            this.class_finalize = class_finalize!;
            this.instance_init = instance_init!;

            // Initialise to Null
            this.class_data = IntPtr.Zero;
            this.value_table = IntPtr.Zero;
            this.n_preallocs = 0;
        }
    }
}