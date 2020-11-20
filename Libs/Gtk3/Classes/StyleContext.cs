using GObject;

namespace Gtk
{
    public partial class StyleContext
    {
        public StyleContext() {}

        public void AddClass(string class_name) => Native.add_class(Handle, class_name);
        public void RemoveClass(string class_name) => Native.remove_class(Handle, class_name);

        public void SetScreen(Gdk.Screen screen) => Native.set_screen(Handle, GetHandle(screen));
        public void GetScreen() => Native.get_screen(Handle);

        // TODO: Add Constants for Priorities
        // 600 => Application Priority
        // 800 => User Priority

        public static void AddProviderForScreen(Gdk.Screen screen, CssProvider provider, uint priority)
            => Native.add_provider_for_screen(GetHandle(screen), GetHandle(provider), priority);

        /// <summary>
        /// StyleContext.AddProvider adds a CssProvider to a single style
        /// context. Note that it is only able to style the widget which owns
        /// this style context, and NOT any child widgets in the tree.
        /// </summary>
        public void AddProvider(CssProvider provider, uint priority) => Native.add_provider(Handle, GetHandle(provider), priority);
    }
}
