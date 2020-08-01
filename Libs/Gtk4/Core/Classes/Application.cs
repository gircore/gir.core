using Gio.Sys;

namespace Gtk
{
    public class Application : Gio.Application
    {
        public Application(string applicationId) : base(Sys.Application.@new(applicationId, ApplicationFlags.flags_none)) {}
    }
}