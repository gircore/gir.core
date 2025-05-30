namespace Rsvg;

public static class Module
{
    private static bool IsInitialized;

    /// <summary>
    /// Initialize the <c>Rsvg</c> module.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Calling this method is necessary to correctly initialize the bindings
    /// and should be done before using anything else in the <see cref="Rsvg" />
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
    /// <item><description><see cref="GLib.Module" /></description></item>
    /// <item><description><see cref="GObject.Module" /></description></item>
    /// </list>
    /// </remarks>
    public static void Initialize()
    {
        if (IsInitialized)
            return;

        Cairo.Module.Initialize();
        GdkPixbuf.Module.Initialize();
        Gio.Module.Initialize();
        GLib.Module.Initialize();
        GObject.Module.Initialize();

        Internal.ImportResolver.RegisterAsDllImportResolver();
        Internal.TypeRegistration.RegisterTypes();

        IsInitialized = true;
    }
}
