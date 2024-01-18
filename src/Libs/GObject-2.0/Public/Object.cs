using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using GLib;
using GObject.Internal;

namespace GObject;

public partial class Object : IObject, INotifyPropertyChanged, IDisposable, IHandle
{
    /// <summary>
    /// Event triggered when a property value changes.
    /// </summary>
    public event PropertyChangedEventHandler? PropertyChanged;

    private readonly ObjectHandle _handle;

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
    /// <param name="owned">True if the ownership of the resulting resulting handle will be transfered. Otherwise false.</param>
    /// <param name="constructArguments"></param>
    /// <remarks>This constructor is protected to be sure that there is no caller (enduser) keeping a reference to
    /// the construct parameters as the contained values are freed at the end of this constructor.
    /// If certain constructors are needed they need to be implemented with concrete constructor arguments in
    /// a higher layer.</remarks>
    protected Object(bool owned, ConstructArgument[] constructArguments)
    {
        Type gtype = GetGTypeOrRegister(GetType());

        IntPtr handle = Internal.Object.NewWithProperties(
            objectType: gtype,
            nProperties: (uint) constructArguments.Length,
            names: GetNames(constructArguments),
            values: ValueArray2OwnedHandle.Create(constructArguments.Select(x => x.Value).ToArray())
        );

        // We can't check if a reference is floating via "g_object_is_floating" here
        // as the function could be "lying" depending on the intent of framework writers.
        // E.g. A Gtk.Window created via "g_object_new_with_properties" returns an unowned
        // reference which is not marked as floating as the gtk toolkit "owns" it.
        // For this reason we just delegate the problem to the caller and require a
        // definition wether the ownership of the new object will be transered to us or not.
        _handle = new ObjectHandle(handle, this, owned);

        Initialize();
    }

    private string[] GetNames(ConstructArgument[] constructParameters)
        => constructParameters.Select(x => x.Name).ToArray();

    /// <summary>
    /// Does common initialization tasks.
    /// Wrapper and subclasses can override here to perform immediate initialization.
    /// </summary>
    protected virtual void Initialize()
    {
        Debug.WriteLine($"Initialising Object: Address {_handle.Handle}, Type {GetType()}.");
    }

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
        DisposeClosures();
        _handle.Dispose();
    }
}
