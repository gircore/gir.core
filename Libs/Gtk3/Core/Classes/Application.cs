namespace Gtk
{
    public partial class Application
    {
        public Application(string applicationId) : base(Sys.Application.@new(applicationId, Gio.Sys.ApplicationFlags.flags_none)) {}

        public void AddWindow(Window window) => Sys.Application.add_window(Handle, GetHandle(window));
        public void SetAppMenu(Menu menu) => Sys.Application.set_app_menu(Handle, GetHandle(menu));
        public void SetMenubar(Menu menu) => Sys.Application.set_menubar(Handle, GetHandle(menu));
    }
}