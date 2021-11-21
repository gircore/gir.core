using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using GLib;
using GObject.Internal;

namespace GObject
{
    public partial class Object : IObject, INotifyPropertyChanged, IDisposable, IHandle
    {
        /// <summary>
        /// Event triggered when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;

        private readonly ObjectHandle _handle;
        private SignalRegistry _signalRegistry;

        public IntPtr Handle => _handle.Handle;

        /// <summary>
        /// Initializes a wrapper for an existing object
        /// </summary>
        /// <param name="handle"></param>
        /// <param name="ownedRef">Defines if the handle is owned by us. If not owned by us it is refed to keep it around.</param>
        protected Object(IntPtr handle, bool ownedRef)
        {
            _handle = new ObjectHandle(handle, this, ownedRef);
            Initialize();
        }

        /// <summary>
        /// Constructs a new object
        /// </summary>
        /// <param name="constructArguments"></param>
        /// <remarks>This constructor is protected to be sure that there is no caller (enduser) keeping a reference to
        /// the construct parameters as the contained values are freed at the end of this constructor.
        /// If certain constructors are needed they need to be implemented with concrete constructor arguments in
        /// a higher layer.</remarks>
        protected Object(ConstructArgument[] constructArguments)
        {
            Type gtype = GetGTypeOrRegister(GetType());

            IntPtr handle = Internal.Object.Instance.Methods.NewWithProperties(
                objectType: gtype.Value,
                nProperties: (uint) constructArguments.Length,
                names: GetNames(constructArguments),
                values: GetValues(constructArguments)
            );

            _handle = new ObjectHandle(handle, this, !Internal.Object.Instance.Methods.IsFloating(handle));

            Initialize();
        }

        private string[] GetNames(ConstructArgument[] constructParameters)
            => constructParameters.Select(x => x.Name).ToArray();

        private Internal.Value.Struct[] GetValues(ConstructArgument[] constructParameters)
        {
            var values = new Internal.Value.Struct[constructParameters.Length];

            for (int i = 0; i < constructParameters.Length; i++)
            {
                values[i] = constructParameters[i].Value.GetData();
            }

            return values;
        }

        /// <summary>
        /// Does common initialization tasks.
        /// Wrapper and subclasses can override here to perform immediate initialization.
        /// </summary>
        [MemberNotNull(nameof(_signalRegistry))]
        protected virtual void Initialize()
        {
            Debug.WriteLine($"Initialising Object: Address {_handle.Handle}, Type {GetType()}.");
            _signalRegistry = new SignalRegistry(this);
        }

        internal ClosureRegistry GetClosureRegistry(string signalName)
            => _signalRegistry.GetClosureRegistry(signalName);

        /// <summary>
        /// Notify this object that a property has just changed.
        /// </summary>
        /// <param name="propertyName">The name of the property who changed.</param>
        protected internal void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            if (propertyName == null)
                return;

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public virtual void Dispose()
        {
            Debug.WriteLine($"Disposing Object: Address {_handle.Handle}, Type {GetType()}.");
            _signalRegistry.Dispose();
            _handle.Dispose();
        }
    }
}
