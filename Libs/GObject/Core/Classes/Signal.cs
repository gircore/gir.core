using System;
using System.Collections.Generic;

namespace GObject
{
    public class SignalArgs : EventArgs
    {
        public object[] Args { get; }

        public SignalArgs()
        {
            Args = Array.Empty<object>();
        }

        internal SignalArgs(params object[] args)
        {
            Args = args;
        }

        internal SignalArgs(params Sys.Value[] args)
        {
            Args = new object[args.Length];

            for (int i = 0; i < args.Length; i++)
            {
                // TODO: Args[i] = args[i].Value;
            }
        }
    }

    /// <summary>
    /// Describes a GSignal.
    /// </summary>
    public sealed class Signal
    {
        #region Fields

        private static readonly Dictionary<EventHandler<SignalArgs>, ActionRefValues> _handlers = new Dictionary<EventHandler<SignalArgs>, ActionRefValues>();

        #endregion

        #region Properties

        /// <summary>
        /// The name of the GSignal.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// The signal flags.
        /// </summary>
        public Sys.SignalFlags Flags { get; }

        /// <summary>
        /// The return type of signal handlers.
        /// </summary>
        public Sys.Type ReturnType { get; }

        /// <summary>
        /// The type of parameters in signal handlers.
        /// </summary>
        public Sys.Type[] ParamTypes { get; }

        #endregion

        #region Constructors

        private Signal(string name, Sys.SignalFlags flags, Sys.Type returnType, Sys.Type[] paramTypes)
        {
            Name = name;
            Flags = flags;
            ReturnType = returnType;
            ParamTypes = paramTypes;
        }

        #endregion

        #region Methods

        public static Signal Register(string name, Sys.SignalFlags flags, Sys.Type returnType, params Sys.Type[] paramTypes)
        {
            return new Signal(name, flags, returnType, paramTypes);
        }

        public static Signal Register(string name, Sys.SignalFlags flags = Sys.SignalFlags.run_last)
        {
            return new Signal(name, flags, Sys.Type.None, Array.Empty<Sys.Type>());
        }

        public void Connect(Object o, Action action, bool after = false)
        {
            if (action == null)
                return;

            o.RegisterEvent(Name, action, after);
        }

        public void Connect(Object o, ActionRefValues action, bool after = false)
        {
            if (action == null)
                return;

            o.RegisterEvent(Name, action, after);
        }

        public void Connect(Object o, EventHandler<SignalArgs> action, bool after = false)
        {
            if (action == null)
                return;

            if (!_handlers.TryGetValue(action, out ActionRefValues callback))
                callback = (ref Sys.Value[] values) => action(o, new SignalArgs(values));

            o.RegisterEvent(Name, callback, after);
            _handlers[action] = callback;
        }

        public void Disconnect(Object o, Action action)
        {
            if (action == null)
                return;

            o.UnregisterEvent(action);
        }

        public void Disconnect(Object o, ActionRefValues action)
        {
            if (action == null)
                return;

            o.UnregisterEvent(action);
        }

        public void Disconnect(Object o, EventHandler<SignalArgs> action)
        {
            if (action == null)
                return;

            if (!_handlers.TryGetValue(action, out ActionRefValues callback))
                return;

            o.UnregisterEvent(callback);
            _handlers.Remove(action);
        }

        #endregion
    }
}