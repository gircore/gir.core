namespace Gtk
{
    public class Application : Gio.Application
    {
        public Application(string applicationId) : base(Sys.Application.@new(applicationId, Gio.Sys.ApplicationFlags.flags_none)) {}
    }
}