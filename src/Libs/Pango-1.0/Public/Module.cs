namespace Pango;

public class Module
{
    private static bool IsInitialized;

    public static void Initialize()
    {
        if (IsInitialized)
            return;

        GObject.Module.Initialize();
        Gio.Module.Initialize();
        Cairo.Module.Initialize();
        HarfBuzz.Module.Initialize();

        Internal.ImportResolver.RegisterAsDllImportResolver();
        Internal.TypeRegistration.RegisterTypes();

        IsInitialized = true;
    }
}
