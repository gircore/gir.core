namespace Gtk
{
    public partial class StyleContext
    {
        public static void AddProviderForScreen(Gdk.Screen screen, CssProvider provider, uint priority)
            => Native.StyleContext.Instance.Methods.AddProviderForScreen(screen.Handle, provider.Handle, priority);

        public static void AddProvider(Gdk.Screen screen, CssProvider provider, uint priority)
            => Native.StyleContext.Instance.Methods.AddProvider(screen.Handle, provider.Handle, priority);
    }
}
