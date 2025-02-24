namespace Samples;

internal static class Program
{
    private static void Main(string[] args)
    {
        Gio.Module.Initialize();
        Sample.DBus.ReadDesktopAppearanceColorScheme();
        Sample.DBus.SendNotification();
    }
}
