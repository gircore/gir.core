namespace GLib
{
    public partial class Error
    {
        public static void ThrowOnError(Internal.ErrorHandle error)
        {
            if (!error.IsInvalid)
                throw new GException(error);
        }
    }
}
