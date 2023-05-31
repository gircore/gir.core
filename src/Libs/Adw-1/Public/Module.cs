namespace Adw;

public class Module
{
    private static bool IsInitialized;

    public static void Initialize()
    {
        if (IsInitialized)
            return;

        Gio.Module.Initialize();
        Gtk.Module.Initialize();

        Internal.ImportResolver.RegisterAsDllImportResolver();
        Internal.TypeRegistration.RegisterTypes();
        Internal.Functions.Init();

        IsInitialized = true;
    }
}
