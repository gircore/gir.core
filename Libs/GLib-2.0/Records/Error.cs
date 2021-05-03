namespace GLib
{
    public partial class Error
    {
        public static void ThrowOnError(Native.Error.Handle error)
        {
            if (!error.IsInvalid)
                throw new GException(error);
        }
    }
}
