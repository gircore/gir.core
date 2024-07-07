using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using GLib;
using GObject.Internal;

namespace GObject;

public partial class Object : IObject, IDisposable, IHandle
{
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
        Debug.WriteLine($"Handle {_handle.Handle}: Initialising object of type {GetType()}.");
    }

    public virtual void Dispose()
    {
        Debug.WriteLine($"Handle {_handle.Handle}: Disposing object of type {GetType()}.");
        DisposeClosures();
        _handle.Dispose();
    }
}
