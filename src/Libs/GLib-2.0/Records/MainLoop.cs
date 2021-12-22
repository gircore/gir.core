using System;

namespace GLib
{
    public sealed partial class MainLoop
    {
        public MainLoop(MainContext context, bool isRunning = false)
            : this(context.Handle, isRunning) { }

        public MainLoop() : this(new Internal.MainContext.Handle(IntPtr.Zero), false) { }

        private MainLoop(Internal.MainContext.Handle context, bool isRunning)
        {
            _handle = Internal.MainLoop.Methods.New(context, isRunning);
        }

        public bool IsRunning() => Internal.MainLoop.Methods.IsRunning(Handle);

        public void Run() => Internal.MainLoop.Methods.Run(Handle);

        public void Quit() => Internal.MainLoop.Methods.Quit(Handle);
    }
}
