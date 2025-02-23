namespace Gio;

public static class Module
{
    private static bool IsInitialized;

    /// <summary>
    /// Initialize the <c>Gio</c> module.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Calling this method is necessary to correctly initialize the bindings
    /// and should be done before using anything else in the <see cref="Gio" />
    /// namespace.
    /// </para>
    /// <para>
    /// Calling this method will also initialize the modules this module
    /// depends on:
    /// </para>
    /// <list type="table">
    /// <item><description><see cref="GObject.Module" /></description></item>
    /// </list>
    /// </remarks>
    public static void Initialize()
    {
        if (IsInitialized)
            return;

        // Set immediately as initialized as static constructors like from Gio.Application 
        // which get called during "TypeRegistration.RegisterTypes" will call this method again
        // resulting in a double execution. A second try would probably make no difference.
        IsInitialized = true;

        GObject.Module.Initialize();

        Internal.ImportResolver.RegisterAsDllImportResolver();
        Internal.TypeRegistration.RegisterTypes();
    }
}
