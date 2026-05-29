using System;
using System.Diagnostics;

namespace GObject;

[Obsolete("This DataStructure is a workaround to keep legacy APIs alive. Do not use it.")]
public struct CreationData
{
    public Internal.ObjectHandle Handle;
    public Action<Object> Setup;
}

[Handle<Internal.ObjectHandle>]
public partial class Object : IDisposable, NativeObject
{
    public Internal.ObjectHandle Handle { get; }

    protected Object(Internal.ObjectHandle handle)
    {
        Handle = handle;
        Handle.AddMemoryPressure();
    }

    [Obsolete("Regular C# constructors on native classes will be removed in a future version. Please see the linked documentation for more details. It contains scenarios and possible solutions to prepare for the upcoming changes.", DiagnosticId = "GirCore1007", UrlFormat = "https://gircore.github.io/docs/integration/diagnostic/1007.html")]
    public Object(params ConstructArgument[] constructArguments) : this(CreateLegacy(constructArguments)) { { } }
    [Obsolete("This constructor is for backwards compatibility only. Do not use it in new code.")]
    protected Object(CreationData data) : this(data.Handle)
    {
        data.Setup(this);
    }

    /// <summary>
    /// Creates a new Object and sets the properties specified by the construct arguments.
    /// </summary>
    /// <param name="constructArguments">The properties to set.</param>
    public static Object NewWithProperties(ConstructArgument[] constructArguments)
    {
        var ptr = Internal.Object.NewWithProperties(GetGType(), constructArguments);
        var handle = new Internal.ObjectHandle(ptr);
        var obj = new Object(handle);

        Internal.InstanceCache.AddToggleRef(obj);
        Internal.Object.Unref(ptr);

        return obj;
    }

    /// <summary>
    /// Creates a new managed Object instance for a given pointer.
    /// </summary>
    public static Object NewFromPointer(System.IntPtr ptr, bool ownsHandle) => (InitiallyUnowned) Internal.InstanceWrapper.WrapHandle<Object>(ptr, ownsHandle);

    public virtual void Dispose()
    {
        Debug.WriteLine($"Handle {Handle.DangerousGetHandle()}: Disposing object of type {GetType()}.");
        Handle.Dispose();
    }

    private static CreationData CreateLegacy(ConstructArgument[] arguments)
    {
        {
            var ptr = Internal.Object.NewWithProperties(GetGType(), arguments);
            var handle = new Internal.ObjectHandle(ptr);

            return new CreationData
            {
                Handle = handle,
                Setup = obj =>
                {
                    Internal.InstanceCache.AddToggleRef(obj);
                    Internal.Object.Unref(obj.Handle.DangerousGetHandle());
                }
            };
        }
    }
}
