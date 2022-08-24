namespace Gtk;

public class Module
{
    private static bool IsInitialized;

    public static void Initialize()
    {
        if (IsInitialized)
            return;

        Gio.Module.Initialize();
        Gdk.Module.Initialize();
        GdkPixbuf.Module.Initialize();

        Internal.ImportResolver.RegisterAsDllImportResolver();
        Internal.TypeRegistration.RegisterTypes();

        Functions.Init();

        IsInitialized = true;
    }
}
