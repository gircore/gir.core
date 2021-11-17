using System;

#nullable enable

namespace GLib.Internal
{

    public partial class ThreadPool
    {

        #region Methods

        public partial class Methods
        {
            public static void Free(IntPtr pool)
            {
                throw new NotImplementedException();
                //TODO Free(pool, true, true); //TODO clarify which parameters are usefull
            }
        }
        #endregion
    }
}
