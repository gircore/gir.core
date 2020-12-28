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
            Handle = @new(context, isRunning);
        }
        
        public bool IsRunning() => is_running(Handle);
        
        public void Run() => run(Handle);
        
        public void Quit() => quit(Handle);
    }
}
