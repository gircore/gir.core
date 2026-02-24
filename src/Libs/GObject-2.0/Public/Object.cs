using System;
using System.Diagnostics;
using GObject.Internal;

namespace GObject;

[Obsolete("This DataStructure is a workaround to keep legacy APIs alive. Do not use it.")]
public struct CreationData
{
    public ObjectHandle Handle;
    public Action<Object> Setup;
}

[GObject.Handle<ObjectHandle>]
public partial class Object : IDisposable, NativeObject
{
    public ObjectHandle Handle { get; }

    protected internal Object(ObjectHandle handle)
    {
        Handle = handle;
        Handle.AddMemoryPressure();
    }

    [Obsolete("Regular C# constructors can't be deeply integrated with GObject. Do not use them to create new instances, but use factory methods like 'New...' instead. If you want to create a subclass and are looking for a constructor to call please use the 'GObject.Subclass' attribute instead. If you want to define a custom constructor create a custom factory method instead and set any instance members in there. Please see the 0.8.0 release notes for more details.")]
    public Object(params ConstructArgument[] constructArguments) : this(CreateLegacy(constructArguments)) { { } }
    [Obsolete("This constructor is for backwards compatibility only. Do not use it in new code.")]
    protected internal Object(CreationData data) : this(data.Handle)
    {
        data.Setup(this);
    }

    public virtual void Dispose()
    {
        Debug.WriteLine($"Handle {Handle.DangerousGetHandle()}: Disposing object of type {GetType()}.");
        Handle.Dispose();
    }

    private static GObject.CreationData CreateLegacy(GObject.ConstructArgument[] arguments)
    {
        {
            var ptr = GObject.Internal.Object.NewWithProperties(GetGType(), arguments);
            var handle = new ObjectHandle(ptr);

            return new GObject.CreationData
            {
                Handle = handle,
                Setup = obj =>
                {
                    GObject.Internal.InstanceCache.AddToggleRef(obj);
                    GObject.Internal.Object.Unref(obj.Handle.DangerousGetHandle());
                }
            };
        }
    }
}
