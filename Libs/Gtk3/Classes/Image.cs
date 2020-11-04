namespace Gtk
{
    public partial class Image
    {
        public static Image FromFile(string filename)
            => new Image(Native.new_from_file(filename));
    }
}
