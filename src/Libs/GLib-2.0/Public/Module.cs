namespace GLib;

public class Module
{
    private static bool IsInitialized;

    public static void Initialize()
    {
        if (IsInitialized)
            return;

        Internal.ImportResolver.RegisterAsDllImportResolver();

        IsInitialized = true;
    }
}
