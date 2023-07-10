namespace GLib;

public class Module
{
    private static bool IsInitialized;

    /// <summary>
    /// Initialize the <c>GLib</c> module.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Calling this method is necessary to correctly initialize the bindings
    /// and should be done before using anything else in the <see cref="GLib" />
    /// namespace.
    /// </para>
    /// </remarks>
    public static void Initialize()
    {
        if (IsInitialized)
            return;

        Internal.ImportResolver.RegisterAsDllImportResolver();

        IsInitialized = true;
    }
}
