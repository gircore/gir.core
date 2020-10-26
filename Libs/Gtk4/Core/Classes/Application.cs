namespace Gtk
{
    public partial class Application
    {
        public Application(string applicationId) : base(Sys.Application.@new(applicationId, Gio.Sys.ApplicationFlags.flags_none)) {}
    }
}