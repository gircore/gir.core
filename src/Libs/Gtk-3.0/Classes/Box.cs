namespace Gtk
{
    public partial class Box
    {
        public static Box New(Orientation orientation, int spacing)
            => new(Internal.Box.New(orientation, spacing), false);
    }
}
