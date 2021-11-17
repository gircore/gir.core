namespace GLib
{
    public partial class Error
    {
        public static void ThrowOnError(Internal.Error.Handle error)
        {
            if (!error.IsInvalid)
                throw new GException(error);
        }
    }
}
