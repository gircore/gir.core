using System;
using System.Runtime.InteropServices;

namespace GLib
{
    public static class Timeout
    {
        // TODO: Revisit doc comments
        
        ///<summary>
        /// Sets a function to be called at regular intervals, with the default
        /// priority, <c>G_PRIORITY_DEFAULT</c>.  The function is called repeatedly
        /// until it returns <c>false</c>, at which point the timeout is automatically
        /// destroyed and the function will not be called again.  The first call
        /// to the function will be at the end of the first <paramref name="interval"/>.
        /// 
        /// Note that timeout functions may be delayed, due to the processing of other
        /// event sources. Thus they should not be relied on for precise timing.
        /// After each call to the timeout function, the time of the next
        /// timeout is recalculated based on the current time and the given interval
        /// (it does not try to 'catch up' time lost in delays).
        /// 
        /// See [memory management of sources][mainloop-memory-management] for details
        /// on how to handle the return value and memory management of @data.
        /// 
        /// If you want to have a timer in the "seconds" range and do not care
        /// about the exact time of the first call of the timer, use the
        /// <c>Timeout.AddSeconds()</c> function; this function allows for more
        /// optimizations and more efficient system power usage.
        /// 
        /// This internally creates a main loop source using <c>Timeout.SourceNew()</c>
        /// and attaches it to the global <c>MainContext</c> using <c>Source.Attach</c>, so
        /// the callback will be invoked in whichever thread is running that main
        /// context. You can do these steps manually if you need greater control or to
        /// use a custom main context.
        /// 
        /// The interval given is in terms of monotonic time, not wall clock
        /// time.  See g_get_monotonic_time().
        ///</summary>
        public static uint Add(uint interval, Func<bool> function)
        {
            // TODO: This is broken (we cannot marshal FuncData)
            var data = new FuncData(function);
            IntPtr ptr = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(FuncData)));
            Marshal.StructureToPtr<FuncData>(data, ptr, false);
            return Global.Native.timeout_add(interval, TimeoutSourceFunction, ptr);
        }

        private static bool TimeoutSourceFunction(IntPtr data)
        {
            FuncData funcData = Marshal.PtrToStructure<FuncData>(data);
            var result = funcData.managedFunction.Invoke();

            if (result == false)
            {
                // When result is false, this will never be called again
                // and GLib will free the underlying memory. Take this
                // opportunity to free FuncData
                Marshal.FreeHGlobal(data);
            }

            return result;
        }

        // TODO: A better method is probably to create an ID for each timeout
        // function and retrieve the corresponding managed function from a
        // dictionary. Consider this a temporary measure to get Hyena
        // to compile.
        internal struct FuncData
        {
            internal readonly Func<bool> managedFunction;

            internal FuncData(Func<bool> function)
                => this.managedFunction = function;
        }
    }
}
