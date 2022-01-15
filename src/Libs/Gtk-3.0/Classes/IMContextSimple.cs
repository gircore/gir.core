namespace Gtk
{
    public partial class IMContextSimple
    {
        public static IMContextSimple New()
            => new IMContextSimple(Internal.IMContextSimple.New(), true);
    }
}
