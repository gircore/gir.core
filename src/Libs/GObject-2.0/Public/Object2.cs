using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
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
            _handle.Cache(this);
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
    public delegate Object2 InstanceFactoryForType(IntPtr handle, bool ownsHandle);

    public interface RegisteredGType
    {
        static abstract Type GetGType();
        static abstract Object2 Create(IntPtr handle, bool ownsHandle);
    }
/// <summary>
/// Registers a custom subclass with the GObject type system.
/// </summary>
public static class SubclassRegistrar2
{
    public static Type Register<TSubclass, TParent>() 
        where TSubclass : RegisteredGType 
        where TParent : RegisteredGType
    {
        var newType = RegisterNewGType<TSubclass, TParent>();
        InstanceFactory.Register(newType, TSubclass.Create);
        
        return newType;
    }

    private static Type RegisterNewGType<TSubclass, TParent>() 
        where TSubclass : RegisteredGType 
        where TParent : RegisteredGType
    {
        var parentType = TParent.GetGType();
        var parentTypeInfo = TypeQueryOwnedHandle.Create();
        Functions.TypeQuery(parentType, parentTypeInfo);

        if (parentTypeInfo.GetType() == 0)
            throw new TypeRegistrationException("Could not query parent type");
        
        Debug.WriteLine($"Registering new type {typeof(TSubclass).FullName} with parent {typeof(TParent).FullName}");

        // Create TypeInfo
        //TODO: Callbacks for "ClassInit" and "InstanceInit" are disabled because if multiple instances
        //of the same type are created, the typeInfo object can get garbagec collected in the mean time
        //and with it the instances of "DoClassInit" and "DoInstanceInit". If the callback occurs the
        //runtime can't do the call anymore and crashes with:
        //A callback was made on a garbage collected delegate of type 'GObject-2.0!GObject.Internal.InstanceInitFunc::Invoke'
        //Fix this by caching the garbage collected instances somehow
        var handle = TypeInfoOwnedHandle.Create();
        handle.SetClassSize((ushort) parentTypeInfo.GetClassSize());
        handle.SetInstanceSize((ushort) parentTypeInfo.GetInstanceSize());
        //handle.SetClassInit();
        //handle.SetInstanceInit();

        var qualifiedName = QualifyName(typeof(TSubclass));
        var typeid = Functions.TypeRegisterStatic(parentType, GLib.Internal.NonNullableUtf8StringOwnedHandle.Create(qualifiedName), handle, 0);

        if (typeid == 0)
            throw new TypeRegistrationException("Type Registration Failed!");

        return new Type(typeid);
    }

    private static string QualifyName(System.Type type)
        => type.ToString()
            .Replace(".", string.Empty)
            .Replace("+", string.Empty)
            .Replace("`", string.Empty)
            .Replace("[", "_")
            .Replace("]", string.Empty)
            .Replace(" ", string.Empty)
            .Replace(",", "_");
    
    /* TODO: Enable if init functions are supported again
    // Default Handler for class initialisation.
    private static void DoClassInit(IntPtr gClass, IntPtr classData)
    {
        Console.WriteLine("Subclass type class initialised!");
    }

    // Default Handler for instance initialisation.
    private static void DoInstanceInit(IntPtr gClass, IntPtr classData)
    {
        Console.WriteLine("Subclass instance initialised!");
    }
    */
}
    
public static class InstanceFactory
{
    private static readonly Dictionary<Type, InstanceFactoryForType> TypeFactories = new();

    internal static Object2 Create(IntPtr handle, bool ownsHandle)
    {
        var type = GetType(handle);
        var instanceFactory = GetInstanceFactory(type);
        return instanceFactory(handle, ownsHandle);
    }

    public static void Register(Type type, InstanceFactoryForType handleWrapper)
    {
        TypeFactories.Add(type, handleWrapper);
    }
    
    private static InstanceFactoryForType GetInstanceFactory(Type gtype)
    {
        if (TypeFactories.TryGetValue(gtype, out InstanceFactoryForType? factory))
            return factory;

        // If gtype is not in the type dictionary, walk up the
        // tree until we find a type that is. As all objects are
        // descended from GObject, we will eventually find a parent
        // type that is registered.

        while (!TypeFactories.TryGetValue(gtype, out factory))
        {
            gtype = new Type(Functions.TypeParent(gtype.Value));
            if (gtype.Value == (nuint) BasicType.Invalid ||
                gtype.Value == (nuint) BasicType.None)
                throw new Exception("Could not retrieve parent type - is the typeid valid?");
        }

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
    
    internal class ToggleRef2 : IDisposable
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
    
    internal static class InstanceCache2
    {
        private static readonly Dictionary<IntPtr, ToggleRef2> Cache = new();
        
        public static bool TryGetObject<T>(IntPtr handle, [NotNullWhen(true)] out T? obj) where T : Object2
        {
            if (Cache.TryGetValue(handle, out ToggleRef2? toggleRef))
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
        
        public static void Add(IntPtr handle, Object2 obj)
        {
            lock (Cache)
            {
                Cache[handle] = new ToggleRef2(obj);
            }

            Debug.WriteLine($"Handle {handle}: Added object of type '{obj.GetType()}' to {nameof(InstanceCache2)}");
        }

        public static void Remove(IntPtr handle)
        {
            lock (Cache)
            {
                if (Cache.Remove(handle, out var toggleRef))
                    toggleRef.Dispose();
            }

            Debug.WriteLine($"Handle {handle}: Removed object from {nameof(InstanceCache2)}.");
        }
    }
    
    public static class InstanceWrapper
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
            
            if (InstanceCache2.TryGetObject(handle, out T? obj))
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

        internal void Cache(Object2 obj)
        {
            InstanceCache2.Add(handle, obj);
        }
        
        protected override bool ReleaseHandle()
        {
            RemoveMemoryPressure();
            InstanceCache2.Remove(handle);
            return true;
        }

        protected internal virtual void AddMemoryPressure() { }
        protected virtual void RemoveMemoryPressure() { }
    }
}



