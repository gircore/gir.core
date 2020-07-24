using Gio;

namespace Gtk.Core
{
    public partial class GApplication : Gio.Core.GApplication
    {
        public GApplication(string applicationId) : base(Gtk.Application.@new(applicationId, ApplicationFlags.flags_none)) {}

        public void AddWindow(GWindow window) => Gtk.Application.add_window(this, window);
        public void SetAppMenu(GMenu menu) => Gtk.Application.set_app_menu(this, menu);
        public void SetMenubar(GMenu menu) => Gtk.Application.set_menubar(this, menu);
    }
}