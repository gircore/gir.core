namespace GdkPixbuf;

public class Module
{
    private static bool IsInitialized;

    public static void Initialize()
    {
        if (IsInitialized)
            return;

        Gio.Module.Initialize();

        Internal.ImportResolver.RegisterAsDllImportResolver();
        Internal.TypeRegistration.RegisterTypes();

        IsInitialized = true;
    }
}
