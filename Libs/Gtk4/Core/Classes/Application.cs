namespace Gtk.Core
{
    public partial class GApplication : Gio.Core.GApplication
    {
        public GApplication(string applicationId) : base(Gtk.Application.@new(applicationId, ApplicationFlags.flags_none)) {}
    }
}