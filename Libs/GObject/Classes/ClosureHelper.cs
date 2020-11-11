using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace GObject
{
    public delegate void ActionRefValues(ref Value[] items);

    internal class ClosureHelper : IDisposable
    {
        #region Fields

        private static readonly Dictionary<Delegate, ClosureHelper> Handlers = new Dictionary<Delegate, ClosureHelper>();

        // We need to store a reference to MarshalCallback to
        // prevent the delegate from being collected by the GC
        private readonly ClosureMarshal _marshalCallback;

        private bool _disposedValue;
        private readonly Action? _callback;
        private readonly ActionRefValues? _callbackRefValues;

        #endregion

        #region Properties

        internal IntPtr Handle { get; private set; }

        #endregion

        #region Constructors

        public ClosureHelper(Object obj, Action callback) : this(obj)
        {
            _callback = callback ?? throw new ArgumentNullException(nameof(callback));
            Handlers[callback] = this;
        }

        public ClosureHelper(Object obj, ActionRefValues callbackRefValues) : this(obj)
        {
            _callbackRefValues = callbackRefValues ?? throw new ArgumentNullException(nameof(callbackRefValues));
            Handlers[callbackRefValues] = this;
        }

        private ClosureHelper(Object obj)
        {
            Handle = Closure.Native.new_object((uint) Marshal.SizeOf(typeof(Closure)), obj.Handle);
            _marshalCallback = MarshalCallback;
            Closure.Native.set_marshal(Handle, _marshalCallback);
        }

        ~ClosureHelper() => Dispose(false);

        #endregion

        #region Methods

        private void MarshalCallback(IntPtr closure, ref Value returnValue, uint nParamValues,
            Value[] paramValues, IntPtr invocationHint, IntPtr marshalData)
        {
            _callback?.Invoke();

            _callbackRefValues?.Invoke(ref paramValues);
        }

        public static bool TryGetByDelegate(Action action, out ClosureHelper closure)
        {
            return Handlers.TryGetValue(action, out closure);
        }

        public static bool TryGetByDelegate(ActionRefValues action, out ClosureHelper closure)
        {
            return Handlers.TryGetValue(action, out closure);
        }

        #endregion

        #region IDisposable Implementation

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                Closure.Native.unref(Handle);
                Handle = IntPtr.Zero;
                _disposedValue = true;
            }
        }

        #endregion
    }
}
