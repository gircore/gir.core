using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using GLib;
using GObject.Internal;

namespace GObject
{
    public class Object2 : IDisposable
    {
        private readonly Object2Handle _handle;

        public Object2(Object2Handle handle)
        {
            _handle = handle;
            _handle.Map(this);
            _handle.AddMemoryPressure();
        }

        public IntPtr GetHandle() => _handle.DangerousGetHandle();
        
        public void Dispose()
        {
            _handle.Dispose();
        }
    }
}


namespace GObject.Internal
{
    public interface CanCreateInstance
    {
        static abstract Object2 Create(IntPtr handle, bool ownsHandle);
    }
    
    public delegate Object2 InstanceFactoryForType(IntPtr handle, bool ownsHandle);
    
public static class InstanceFactory
{
    private static readonly Dictionary<Type, InstanceFactoryForType> _reverseTypeDict = new();

    internal static Object2 Create(IntPtr handle, bool ownsHandle)
    {
        var type = GetType(handle);
        var instanceFactory = GetInstanceFactory(type);
        return instanceFactory(handle, ownsHandle);
    }

    public static void Register(Type type, InstanceFactoryForType handleWrapper)
    {
        _reverseTypeDict.Add(type, handleWrapper);
    }
    
    private static InstanceFactoryForType GetInstanceFactory(Type gtype)
    {
        if (_reverseTypeDict.TryGetValue(gtype, out InstanceFactoryForType? factory))
            return factory;

        // If gtype is not in the type dictionary, walk up the
        // tree until we find a type that is. As all objects are
        // descended from GObject, we will eventually find a parent
        // type that is registered.

        while (!_reverseTypeDict.TryGetValue(gtype, out factory))
        {
            gtype = new Type(Functions.TypeParent(gtype.Value));
            if (gtype.Value == (nuint) BasicType.Invalid ||
                gtype.Value == (nuint) BasicType.None)
                throw new Exception("Could not retrieve parent type - is the typeid valid?");
        }

        // Store for future lookups
        _reverseTypeDict[gtype] = factory;

        return factory;
    }
    
    private static unsafe Type GetType(IntPtr handle)
    {
        var gclass = Unsafe.AsRef<TypeInstanceData>((void*) handle).GClass;
        var gtype = Unsafe.AsRef<TypeClassData>((void*) gclass).GType;

        if (gtype == 0)
            throw new Exception("Could not retrieve type from class struct - is the struct valid?");

        return new Type(gtype);
    }
}
    
    public class ToggleRef2 : IDisposable
    {
        private readonly IntPtr _handle;
        private readonly ToggleNotify _callback;

        private object _reference;
        
        public object? Object
        {
            get
            {
                if(_reference is WeakReference weakRef)
                    return weakRef.Target;
                
                return _reference;
            }
        }

        /// <summary>
        /// Initializes a toggle ref. The given object must be already owned by C# as the owned
        /// reference is exchanged with a toggling reference meaning the toggle reference is taking control
        /// over the reference.
        /// This object saves a strong reference to the given object which prevents it from beeing garbage
        /// collected. This strong reference is hold as long as there are other than our own toggling ref
        /// on the given object.
        /// If our toggeling ref is the last ref on the given object the strong reference is changed into a
        /// weak reference. This allows the garbage collector to free the C# object which must result in the
        /// call of the Dispose method of the ToggleRef. The Dispose method removes the added toggle reference
        /// and thus frees the last reference to the C object.
        /// </summary>
        public ToggleRef2(Object2 obj)
        {
            _reference = obj;
            _handle = obj.GetHandle();

            _callback = ToggleReference;

            RegisterToggleRef();
        }

        private void RegisterToggleRef()
        {
            Internal.Object.AddToggleRef(_handle, _callback, IntPtr.Zero);
            Internal.Object.Unref(_handle);
        }

        private void ToggleReference(IntPtr data, IntPtr @object, bool isLastRef)
        {
            if (!isLastRef && _reference is WeakReference weakRef)
            {
                if (weakRef.Target is { } weakObj)
                    _reference = weakObj;
                else
                    throw new Exception($"Handle {_handle}: Could not toggle reference to strong. It got garbage collected.");
            }
            else if (isLastRef && _reference is not WeakReference)
            {
                _reference = new WeakReference(_reference);
            }
        }

        public void Dispose()
        {
            var sourceFunc = new GLib.Internal.SourceFuncAsyncHandler(() =>
            {
                Internal.Object.RemoveToggleRef(_handle, _callback, IntPtr.Zero);
                return false;
            });
            GLib.Internal.MainContext.Invoke(GLib.Internal.MainContextUnownedHandle.NullHandle, sourceFunc.NativeCallback, IntPtr.Zero);
        }
    }
    
    public class ObjectMapper2
    {
        private static readonly Dictionary<IntPtr, ToggleRef2> WrapperObjects = new();
        
        public static bool TryGetObject<T>(IntPtr handle, [NotNullWhen(true)] out T? obj) where T : Object2
        {
            if (WrapperObjects.TryGetValue(handle, out ToggleRef2? toggleRef))
            {
                if (toggleRef.Object is not null)
                {
                    obj = (T) toggleRef.Object;
                    return true;
                }
            }

            obj = null;
            return false;
        }
        
        public static void Map(IntPtr handle, Object2 obj)
        {
            lock (WrapperObjects)
            {
                WrapperObjects[handle] = new ToggleRef2(obj);
            }

            Debug.WriteLine($"Handle {handle}: Mapped object of type '{obj.GetType()}'");
        }

        public static void Unmap(IntPtr handle)
        {
            lock (WrapperObjects)
            {
                if (WrapperObjects.Remove(handle, out var toggleRef))
                    toggleRef.Dispose();
            }

            Debug.WriteLine($"Handle {handle}: Unmapped object.");
        }
    }
    
    public class ObjectWrapper2
    {
        public static T? WrapNullableHandle<T>(IntPtr handle, bool ownedRef) where T : Object2
        {
            return handle == IntPtr.Zero
                ? null
                : WrapHandle<T>(handle, ownedRef);
        }
        
        public static T WrapHandle<T>(IntPtr handle, bool ownedRef) where T : Object2
        {
            if (handle == IntPtr.Zero)
                throw new NullReferenceException($"Failed to wrap handle as type <{typeof(T).FullName}>. Null handle passed to WrapHandle.");
            
            if (ObjectMapper2.TryGetObject(handle, out T? obj))
                return obj;

            return (T) InstanceFactory.Create(handle, ownedRef);
        }
        

        
    }
    
    public class Object2Handle : SafeHandle
    {
        public override bool IsInvalid => handle == IntPtr.Zero;
    
        public Object2Handle(IntPtr handle, bool ownsHandle) : base(IntPtr.Zero, true)
        {
            SetHandle(handle);
            OwnReference(ownsHandle);
        }

        private void OwnReference(bool ownedRef)
        {
            if (!ownedRef)
            {
                // - Unowned GObjects need to be refed to bind them to this instance
                // - Unowned InitiallyUnowned floating objects need to be ref_sinked
                // - Unowned InitiallyUnowned non-floating objects need to be refed
                // As ref_sink behaves like ref in case of non floating instances we use it for all 3 cases
                Object.RefSink(handle);
            }
            else
            {
                //In case we own the ref because the ownership was fully transfered to us we
                //do not need to ref the object at all.

                Debug.Assert(!Internal.Object.IsFloating(handle), $"Handle {handle}: Owned floating references are not possible.");
            }
        }

        internal void Map(Object2 obj)
        {
            ObjectMapper2.Map(handle, obj);
        }
        
        protected override bool ReleaseHandle()
        {
            RemoveMemoryPressure();
            ObjectMapper2.Unmap(handle);
            return true;
        }

        protected internal virtual void AddMemoryPressure() { }
        protected virtual void RemoveMemoryPressure() { }
    }
}



