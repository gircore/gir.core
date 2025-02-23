namespace GstPbutils;

public static class Module
{
    private static bool IsInitialized;

    /// <summary>
    /// Initialize the <c>GstPbutils</c> module.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Calling this method is necessary to correctly initialize the bindings
    /// and should be done before using anything else in the <see cref="GstPbutils" />
    /// namespace.
    /// </para>
    /// <para>
    /// Calling this method will also initialize the modules this module
    /// depends on:
    /// </para>
    /// <list type="table">
    /// <item><description><see cref="Gst.Module" /></description></item>
    /// <item><description><see cref="GstBase.Module" /></description></item>
    /// <item><description><see cref="GstAudio.Module" /></description></item>
    /// <item><description><see cref="GstVideo.Module" /></description></item>
    /// </list>
    /// </remarks>
    public static void Initialize()
    {
        if (IsInitialized)
            return;

        Gst.Module.Initialize();
        GstBase.Module.Initialize();
        GstAudio.Module.Initialize();
        GstVideo.Module.Initialize();

        Internal.ImportResolver.RegisterAsDllImportResolver();
        Internal.TypeRegistration.RegisterTypes();

        IsInitialized = true;
    }
}
