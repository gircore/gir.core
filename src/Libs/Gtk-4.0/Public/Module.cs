namespace Gtk;

public static class Module
{
    private static bool IsInitialized;

    /// <summary>
    /// Initialize the <c>Gtk</c> module.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Calling this method is necessary to correctly initialize the bindings
    /// and should be done before using anything else in the <see cref="Gtk" />
    /// namespace.
    /// </para>
    /// <para>
    /// Calling this method will also initialize the modules this module
    /// depends on:
    /// </para>
    /// <list type="table">
    /// <item><description><see cref="Gdk.Module" /></description></item>
    /// <item><description><see cref="Gsk.Module" /></description></item>
    /// </list>
    /// <para>
    /// If the <see cref="Gtk.Application" /> class is used there is no need to
    /// call this method, because this module will be implicitly initialized
    /// when accessing <see cref="Gtk.Application" /> the first time.
    /// </para>
    /// </remarks>
    public static void Initialize()
    {
        if (IsInitialized)
            return;

        Gdk.Module.Initialize();
        Gsk.Module.Initialize();

        Internal.ImportResolver.RegisterAsDllImportResolver();
        Internal.TypeRegistration.RegisterTypes();
        Internal.Functions.Init();

        IsInitialized = true;
    }
}
