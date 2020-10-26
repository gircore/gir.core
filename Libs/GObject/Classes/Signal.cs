using System;
using System.Collections.Generic;

namespace GObject
{
    /// <summary>
    /// SignalHandler for signals without any extra data.
    /// </summary>
    /// <param name="sender">The sender of this signal.</param>
    /// <param name="args">Event args. Will always have the value of <see cref="EventArgs.Empty"/>.</param>
    public delegate void SignalHandler<TSender>(TSender sender, EventArgs args)
        where TSender : Object;

    /// <summary>
    /// SignalHandler for signals with extra data.
    /// </summary>
    /// <param name="sender">The sender of this signal.</param>
    /// <param name="args"><see cref="SignalArgs"/> with additional data.</param>
    public delegate void SignalHandler<TSender, TSignalArgs>(TSender sender, TSignalArgs args)
        where TSender : Object
        where TSignalArgs : SignalArgs;

    /// <summary>
    /// Base class for signal based events.
    /// </summary>
    public class SignalArgs : EventArgs
    {
        #region Properties

        protected Value[] Args { get; set; }

        #endregion

        #region Constructors

        public SignalArgs()
        {
            Args = Array.Empty<Value>();
        }

        internal SignalArgs(params Value[] args)
            : this()
        {
            SetArgs(args);
        }

        #endregion

        #region Methods

        internal void SetArgs(Value[] args)
        {
            Args = args;
        }

        #endregion
    }

    public class Signal
    {
        #region Properties

        /// <summary>
        /// The name of the GSignal.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// The signal flags.
        /// </summary>
        public SignalFlags Flags { get; }

        /// <summary>
        /// The return type of signal handlers.
        /// </summary>
        public Type ReturnType { get; }

        /// <summary>
        /// The type of parameters in signal handlers.
        /// </summary>
        public Type[] ParamTypes { get; }

        #endregion

        #region Constructors

        internal Signal(string name, SignalFlags flags, Type returnType, Type[] paramTypes)
        {
            Name = name;
            Flags = flags;
            ReturnType = returnType;
            ParamTypes = paramTypes;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Registers a new GSignal into this type.
        /// </summary>
        /// <param name="name">The name of the GSignal to create.</param>
        /// <param name="flags">The GSignal flags.</param>
        /// <param name="returnType">The type of the value returned by the handlers of this GSignal.</param>
        /// <param name="paramTypes">
        /// The types list for each parameters given to handlers of this GSignal,
        /// in the order they appear.</param>
        /// <returns>
        /// An instance of <see cref="Signal"/> which describes the registered signal.
        /// </returns>
        public static Signal Register(string name, SignalFlags flags, Type returnType, params Type[] paramTypes)
        {
            return new Signal(name, flags, returnType, paramTypes);
        }

        /// <summary>
        /// Registers a new GSignal into this type.
        /// </summary>
        /// <param name="name">The name of the GSignal to create.</param>
        /// <param name="flags">The GSignal flags.</param>
        /// <returns>
        /// An instance of <see cref="Signal"/> which describes the registered signal.
        /// </returns>
        public static Signal Register(string name, SignalFlags flags = SignalFlags.RunLast)
        {
            return new Signal(name, flags, Type.None, Array.Empty<Type>());
        }

        /// <summary>
        /// Wraps an existing GSignal.
        /// </summary>
        /// <param name="name">The name of the GSignal to wrap.</param>
        /// <returns>
        /// An instance of <see cref="Signal"/> which describes the signal to wrap.
        /// </returns>
        public static Signal Wrap(string name)
        {
            // Here only the signal name is relevant, other parameters are not used.
            return new Signal(name, SignalFlags.RunLast, Type.None, Array.Empty<Type>());
        }

        /// <summary>
        /// Connects an <paramref name="action"/> to this signal.
        /// </summary>
        /// <param name="o">The object on which connect the handler.</param>
        /// <param name="action">The signal handler function.</param>
        /// <param name="after">
        /// Define if this action must be called before or after the default handler of this signal.
        /// </param>
        public void Connect(Object o, Action action, bool after = false)
        {
            if (action == null)
                return;

            o.RegisterEvent(Name, action, after);
        }

        /// <summary>
        /// Connects an <paramref name="action"/> to this signal.
        /// </summary>
        /// <param name="o">The object on which connect the handler.</param>
        /// <param name="action">The signal handler function.</param>
        /// <param name="after">
        /// Define if this action must be called before or after the default handler of this signal.
        /// </param>
        public void Connect(Object o, ActionRefValues action, bool after = false)
        {
            if (action == null)
                return;

            o.RegisterEvent(Name, action, after);
        }

        /// <summary>
        /// Disconnects an <paramref name="action"/> previously connected to this signal.
        /// </summary>
        /// <param name="o">The object from which disconnect the handler.</param>
        /// <param name="action">The signal handler function.</param>
        public void Disconnect(Object o, Action action)
        {
            if (action == null)
                return;

            o.UnregisterEvent(action);
        }

        /// <summary>
        /// Disconnects an <paramref name="action"/> previously connected to this signal.
        /// </summary>
        /// <param name="o">The object from which disconnect the handler.</param>
        /// <param name="action">The signal handler function.</param>
        public void Disconnect(Object o, ActionRefValues action)
        {
            if (action == null)
                return;

            o.UnregisterEvent(action);
        }
        #endregion
    }

    /// <summary>
    /// Describes a GSignal.
    /// </summary>
    public class Signal<TSender, TSignalArgs> : Signal
        where TSender : Object
        where TSignalArgs : SignalArgs, new()
    {
        #region Fields

        private static readonly Dictionary<SignalHandler<TSender, TSignalArgs>, ActionRefValues> Handlers =
            new Dictionary<SignalHandler<TSender, TSignalArgs>, ActionRefValues>();

        #endregion

        #region Constructors

        internal Signal(string name, SignalFlags flags, Type returnType, Type[] paramTypes)
            : base(name, flags, returnType, paramTypes)
        { }

        #endregion

        #region Methods

        /// <summary>
        /// Wraps an existing GSignal.
        /// </summary>
        /// <param name="name">The name of the GSignal to wrap.</param>
        /// <returns>
        /// An instance of <see cref="Signal"/> which describes the signal to wrap.
        /// </returns>
        public new static Signal<TSender, TSignalArgs> Wrap(string name)
        {
            // Here only the signal name is relevant, other parameters are not used.
            return new Signal<TSender, TSignalArgs>(name, SignalFlags.RunLast, Type.None, Array.Empty<Type>());
        }

        /// <summary>
        /// Connects an <paramref name="action"/> to this signal.
        /// </summary>
        /// <param name="o">The object on which connect the handler.</param>
        /// <param name="action">The signal handler function.</param>
        /// <param name="after">
        /// Define if this action must be called before or after the default handler of this signal.
        /// </param>
        public void Connect(TSender o, SignalHandler<TSender, TSignalArgs> action, bool after = false)
        {
            if (action == null)
                return;

            if (!Handlers.TryGetValue(action, out ActionRefValues callback))
            {
                callback = (ref Value[] values) =>
                {
                    var args = new TSignalArgs();
                    args.SetArgs(values);
                    action(o, args);
                };
            }

            o.RegisterEvent(Name, callback, after);
            Handlers[action] = callback;
        }

        /// <summary>
        /// Disconnects an <paramref name="action"/> previously connected to this signal.
        /// </summary>
        /// <param name="o">The object from which disconnect the handler.</param>
        /// <param name="action">The signal handler function.</param>
        public void Disconnect(TSender o, SignalHandler<TSender, TSignalArgs> action)
        {
            if (action == null)
                return;

            if (!Handlers.TryGetValue(action, out ActionRefValues callback))
                return;

            o.UnregisterEvent(callback);
            Handlers.Remove(action);
        }

        #endregion

    }

    /// <summary>
    /// Describes a GSignal.
    /// </summary>
    public class Signal<TSender> : Signal
        where TSender : Object
    {
        #region Fields

        private static readonly Dictionary<SignalHandler<TSender>, ActionRefValues> Handlers =
            new Dictionary<SignalHandler<TSender>, ActionRefValues>();

        #endregion

        #region Constructors

        internal Signal(string name, SignalFlags flags, Type returnType, Type[] paramTypes)
            : base(name, flags, returnType, paramTypes)
        { }

        #endregion

        #region Methods

        /// <summary>
        /// Wraps an existing GSignal.
        /// </summary>
        /// <param name="name">The name of the GSignal to wrap.</param>
        /// <returns>
        /// An instance of <see cref="Signal"/> which describes the signal to wrap.
        /// </returns>
        public new static Signal<TSender> Wrap(string name)
        {
            // Here only the signal name is relevant, other parameters are not used.
            return new Signal<TSender>(name, SignalFlags.RunLast, Type.None, Array.Empty<Type>());
        }

        /// <summary>
        /// Connects an <paramref name="action"/> to this signal.
        /// </summary>
        /// <param name="o">The object on which connect the handler.</param>
        /// <param name="action">The signal handler function.</param>
        /// <param name="after">
        /// Define if this action must be called before or after the default handler of this signal.
        /// </param>
        public void Connect(TSender o, SignalHandler<TSender> action, bool after = false)
        {
            if (action == null)
                return;

            if (!Handlers.TryGetValue(action, out ActionRefValues callback))
            {
                callback = (ref Value[] _) => action(o, EventArgs.Empty);
            }

            o.RegisterEvent(Name, callback, after);
            Handlers[action] = callback;
        }

        /// <summary>
        /// Disconnects an <paramref name="action"/> previously connected to this signal.
        /// </summary>
        /// <param name="o">The object from which disconnect the handler.</param>
        /// <param name="action">The signal handler function.</param>
        public void Disconnect(TSender o, SignalHandler<TSender> action)
        {
            if (action == null)
                return;

            if (!Handlers.TryGetValue(action, out ActionRefValues callback))
                return;

            o.UnregisterEvent(callback);
            Handlers.Remove(action);
        }

        #endregion
    }
}
