namespace GLib
{
    public sealed partial class MainLoop
    {
        public MainLoop(MainContext context, bool isRunning = false)
            : this(context.Handle, isRunning) { }

        public MainLoop() : this(Internal.MainContextHandle.Null, false) { }

        private MainLoop(Internal.MainContextHandle context, bool isRunning)
        {
            _handle = Internal.MainLoop.New(context, isRunning);
        }

        public bool IsRunning() => Internal.MainLoop.IsRunning(Handle);

        public void Run() => Internal.MainLoop.Run(Handle);

        public void Quit() => Internal.MainLoop.Quit(Handle);
    }
}
