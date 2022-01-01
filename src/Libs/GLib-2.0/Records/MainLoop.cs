using System;

namespace GLib
{
    public sealed partial class MainLoop
    {
        public MainLoop(MainContext context, bool isRunning = false)
            : this(context.Handle, isRunning) { }

        public MainLoop() : this(Internal.MainContext.Handle.Null, false) { }

        private MainLoop(Internal.MainContext.Handle context, bool isRunning)
        {
            _handle = Internal.MainLoop.Methods.New(context, isRunning);
        }

        public bool IsRunning() => Internal.MainLoop.Methods.IsRunning(Handle);

        public void Run() => Internal.MainLoop.Methods.Run(Handle);

        public void Quit() => Internal.MainLoop.Methods.Quit(Handle);
    }
}
