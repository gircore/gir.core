using System;

namespace GLib
{
    public sealed partial class MainLoop
    {
        public readonly IntPtr Handle;
        
        // TODO: Generate main section doc comments for constructors?
        
        ///<summary>
        /// Creates a new <c>GMainLoop</c> structure.
        ///</summary>
        public MainLoop(MainContext context, bool isRunning = false)
            : this(context.Handle, isRunning) { }
        
        ///<summary>
        /// Creates a new <c>GMainLoop</c> structure.
        ///</summary>
        public MainLoop() : this(IntPtr.Zero, false) { }
        
        private MainLoop(IntPtr context, bool isRunning)
        {
            Handle = @new(context, isRunning);
        }

        /// <summary>
        /// Checks to see if the main loop is currently being run via <c>MainLoop.Run()</c>.
        /// <see cref="Run"/>
        /// </summary>
        public bool IsRunning() => is_running(Handle);
        
        /// <summary>
        /// Runs a main loop until <c>MainLoop.Quit()</c> is called on the loop.
        /// If this is called for the thread of the loop's <c>MainContext</c>,
        /// it will process events from the loop, otherwise it will
        /// simply wait.
        /// <see cref="MainLoop.Quit"/>
        /// <see cref="MainContext"/>
        /// </summary>
        public void Run() => run(Handle);
        
        /// <summary>
        /// Stops a <c>MainLoop</c> from running. Any calls to <c>MainLoop.Run()</c>
        /// for the loop will return.
        /// 
        /// Note that sources that have already been dispatched when
        /// <c>MainLoop.Quit()</c> is called will still be executed.
        /// <see cref="MainLoop.Run"/>
        /// </summary>
        public void Quit() => quit(Handle);
    }
}
