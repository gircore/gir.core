namespace GLib
{
    public partial record Error
    {
        public static void ThrowOnError(Native.ErrorSafeHandle error)
        {
            if (!error.IsInvalid)
                throw new GException(error);
        }
    }
}
