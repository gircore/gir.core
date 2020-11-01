namespace Gtk
{
    public partial class ApplicationWindow
    {
        public ApplicationWindow(Application application) : this(Native.@new(GetHandle(application))) { }
    }
}
