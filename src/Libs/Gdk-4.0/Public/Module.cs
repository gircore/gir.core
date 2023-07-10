using System.Runtime.InteropServices;
using System.Threading;

namespace Gdk;

public class Module
{
    private static bool IsInitialized;

    /// <summary>
    /// Initialize the <c>Gdk</c> module.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Calling this method is necessary to correctly initialize the bindings
    /// and should be done before using anything else in the <see cref="Gdk" />
    /// namespace.
    /// </para>
    /// <para>
    /// Calling this method will also initialize the modules this module
    /// depends on:
    /// </para>
    /// <list type="table">
    /// <item><description><see cref="Cairo.Module" /></description></item>
    /// <item><description><see cref="GdkPixbuf.Module" /></description></item>
    /// <item><description><see cref="Gio.Module" /></description></item>
    /// <item><description><see cref="Pango.Module" /></description></item>
    /// <item><description><see cref="PangoCairo.Module" /></description></item>
    /// </list>
    /// </remarks>
    public static void Initialize()
    {
        if (IsInitialized)
            return;

        Cairo.Module.Initialize();
        GdkPixbuf.Module.Initialize();
        Gio.Module.Initialize();
        Pango.Module.Initialize();
        PangoCairo.Module.Initialize();

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
