namespace WebKit2WebExtension;

public class Module
{
    private static bool IsInitialized;

    /// <summary>
    /// Initialize the <c>WebKit2WebExtension</c> module.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Calling this method is necessary to correctly initialize the bindings
    /// and should be done before using anything else in the <see cref="WebKit2WebExtension" />
    /// namespace.
    /// </para>
    /// <para>
    /// Calling this method will also initialize the modules this module
    /// depends on:
    /// </para>
    /// <list type="table">
    /// <item><description><see cref="Gtk.Module" /></description></item>
    /// <item><description><see cref="JavaScriptCore.Module" /></description></item>
    /// <item><description><see cref="Soup.Module" /></description></item>
    /// </list>
    /// </remarks>
    public static void Initialize()
    {
        if (IsInitialized)
            return;

        Gtk.Module.Initialize();
        JavaScriptCore.Module.Initialize();
        Soup.Module.Initialize();

        Internal.ImportResolver.RegisterAsDllImportResolver();
        Internal.TypeRegistration.RegisterTypes();

        IsInitialized = true;
    }
}
