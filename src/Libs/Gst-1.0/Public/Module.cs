namespace Gst;

public class Module
{
    private static bool IsInitialized;

    public static void Initialize()
    {
        if (IsInitialized)
            return;

        GObject.Module.Initialize();
        GLib.Module.Initialize();

        Internal.ImportResolver.RegisterAsDllImportResolver();
        Internal.TypeRegistration.RegisterTypes();

        IsInitialized = true;
    }
}
