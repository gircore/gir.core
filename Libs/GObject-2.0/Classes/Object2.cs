using System;

namespace GObject
{
    public partial class Object2
    {
        public static T WrapHandle<T>(IntPtr handle, bool ownedRef) where T : class
        {
            if (handle == IntPtr.Zero)
                throw new NullReferenceException($"Failed to wrap handle as type <{typeof(T).FullName}>. Null handle passed to WrapHandle.");

            return default;
        }
    }
}
