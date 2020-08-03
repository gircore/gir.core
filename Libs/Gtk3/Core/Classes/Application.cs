namespace Gtk
{
    public partial class Application : Gio.Application
    {
        public Application(string applicationId) : base(Sys.Application.@new(applicationId, Gio.Sys.ApplicationFlags.flags_none)) {}

        public void AddWindow(Window window) => Sys.Application.add_window(this, window);
        public void SetAppMenu(Menu menu) => Sys.Application.set_app_menu(this, menu);
        public void SetMenubar(Menu menu) => Sys.Application.set_menubar(this, menu);
    }
}