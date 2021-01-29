using System;

namespace GLib
{
    public sealed partial class MainLoop : IHandle
    {
        public IntPtr Handle { get; private set; }

        public MainLoop(MainContext context, bool isRunning = false)
            : this(context.Handle, isRunning) { }

        public MainLoop() : this(IntPtr.Zero, false) { }

        private MainLoop(IntPtr context, bool isRunning)
        {
            Handle = Native.@new(context, isRunning);
        }

        public bool IsRunning() => Native.is_running(Handle);

        public void Run() => Native.run(Handle);

        public void Quit() => Native.quit(Handle);
    }
}
