namespace GLib;

public sealed partial class MainLoop
{
    public MainLoop(MainContext context, bool isRunning = false)
        : this(context.Handle, isRunning) { }

    public MainLoop() : this(Internal.MainContextNullHandle.Instance, false) { }

    private MainLoop(Internal.MainContextHandle context, bool isRunning)
    {
        _handle = Internal.MainLoop.New(context, isRunning);
    }

    public MainContext GetContext()
    {
        return new MainContext(Internal.MainLoop.GetContext(Handle));
    }

    public bool IsRunning() => Internal.MainLoop.IsRunning(Handle);

    public void Run() => Internal.MainLoop.Run(Handle);

    public void Quit() => Internal.MainLoop.Quit(Handle);
}
