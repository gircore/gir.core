using System;

namespace GLib
{
    public sealed partial class MainLoop
    {
        public readonly IntPtr Handle;
        
        // TODO: Generate main section doc comments for constructors?
        
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
