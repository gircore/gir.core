namespace Gtk;

public class Module
{
    private static bool IsInitialized;

    public static void Initialize()
    {
        if (IsInitialized)
            return;

        Gio.Module.Initialize();
        GdkPixbuf.Module.Initialize();
        Gdk.Module.Initialize();
        Cairo.Module.Initialize();

        Internal.ImportResolver.RegisterAsDllImportResolver();
        Internal.TypeRegistration.RegisterTypes();

        IsInitialized = true;
    }
}
