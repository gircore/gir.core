namespace GLib
{
    public partial record Error
    {
        public static void ThrowOnError(Native.Error.Handle error)
        {
            if (!error.IsInvalid)
                throw new GException(error);
        }
    }
}
