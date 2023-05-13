using System.Runtime.InteropServices;
using System.Threading;

namespace Gdk;

public class Module
{
    private static bool IsInitialized;

    public static void Initialize()
    {
        if (IsInitialized)
            return;

        GObject.Module.Initialize();

        Internal.ImportResolver.RegisterAsDllImportResolver();
        Internal.TypeRegistration.RegisterTypes();

        // On Windows, GDK requires the main thread's apartment state to be STA.
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            // In order to successfully switch from MTA to STA, it is necessary
            // to first set it to Unknown.
            Thread.CurrentThread.SetApartmentState(ApartmentState.Unknown);
            Thread.CurrentThread.SetApartmentState(ApartmentState.STA);
        }

        IsInitialized = true;
    }
}
