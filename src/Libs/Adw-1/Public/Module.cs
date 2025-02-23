namespace Adw;

public static class Module
{
    private static bool IsInitialized;

    /// <summary>
    /// Initialize the <c>Adw</c> module.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Calling this method is necessary to correctly initialize the bindings
    /// and should be done before using anything else in the <see cref="Adw" />
    /// namespace.
    /// </para>
    /// <para>
    /// Calling this method will also initialize the modules this module
    /// depends on:
    /// </para>
    /// <list type="table">
    /// <item><description><see cref="Gio.Module" /></description></item>
    /// <item><description><see cref="Gtk.Module" /></description></item>
    /// </list>
    /// <para>
    /// If the <see cref="Adw.Application" /> class is used there is no need to
    /// call this method, because this module will be implicitly initialized
    /// when accessing <see cref="Adw.Application" /> the first time.
    /// </para>
    /// </remarks>
    public static void Initialize()
    {
        if (IsInitialized)
            return;

        // Set immediately as initialized as static constructors like from Gio.Application 
        // which get called during "TypeRegistration.RegisterTypes" will call this method again
        // resulting in a double execution. A second try would probably make no difference.
        IsInitialized = true;

        Gio.Module.Initialize();
        Gtk.Module.Initialize();

        Internal.ImportResolver.RegisterAsDllImportResolver();
        Internal.TypeRegistration.RegisterTypes();
        Internal.Functions.Init();
    }
}
