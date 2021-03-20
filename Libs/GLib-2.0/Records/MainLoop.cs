using System;

namespace GLib
{
    public sealed partial record MainLoop
    {
        public Native.MainLoopSafeHandle Handle { get; private set; }

        public MainLoop(MainContext context, bool isRunning = false)
            : this(context.Handle, isRunning) { }

        public MainLoop() : this(new MainContext.Native.MainContextSafeHandle(IntPtr.Zero), false) { }

        private MainLoop(MainContext.Native.MainContextSafeHandle context, bool isRunning)
        {
            Handle = Native.Methods.New(context, isRunning);
        }

        public bool IsRunning() => Native.Methods.IsRunning(Handle);

        public void Run() => Native.Methods.Run(Handle);

        public void Quit() => Native.Methods.Quit(Handle);
    }
}
