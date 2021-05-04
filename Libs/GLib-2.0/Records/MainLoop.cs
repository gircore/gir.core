using System;

namespace GLib
{
    public sealed partial class MainLoop
    {
        public MainLoop(MainContext context, bool isRunning = false)
            : this(context.Handle, isRunning) { }

        public MainLoop() : this(new Native.MainContext.Handle(IntPtr.Zero), false) { }

        private MainLoop(Native.MainContext.Handle context, bool isRunning)
        {
            _handle = Native.MainLoop.Methods.New(context, isRunning);
        }

        public bool IsRunning() => Native.MainLoop.Methods.IsRunning(Handle);

        public void Run() => Native.MainLoop.Methods.Run(Handle);

        public void Quit() => Native.MainLoop.Methods.Quit(Handle);
    }
}
