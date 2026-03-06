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

    [Obsolete("Regular C# constructors on native classes will be removed in a future version. Please see the linked documentation for more details. It contains scenarios and possible solutions to prepare for the upcoming changes.", DiagnosticId = "GirCore1007", UrlFormat = "https://gircore.github.io/docs/integration/diagnostic/1007.html")]
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
