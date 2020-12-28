using System;
using System.Runtime.InteropServices;
using Gdk;
using GObject;

namespace Gtk
{
    public partial class AccelGroup
    {
        #region Properties

        #region IsLockedProperty

        /// <summary>
        /// Property descriptor for the <see cref="IsLocked"/> property.
        /// </summary>
        public static readonly Property<bool> IsLockedProperty = Property<bool>.Wrap<AccelGroup>(
            Native.IsLockedProperty,
            nameof(IsLocked),
            get: (o) => o.IsLocked
        );

        public bool IsLocked
        {
            get => GetProperty(IsLockedProperty);
        }

        #endregion

        #region ModifierMaskProperty

        /// <summary>
        /// Property descriptor for the <see cref="ModifierMask"/> property.
        /// </summary>
        public static readonly Property<Gdk.ModifierType> ModifierMaskProperty =
            Property<Gdk.ModifierType>.Wrap<AccelGroup>(
                Native.ModifierMaskProperty,
                nameof(ModifierMask),
                get: (o) => o.ModifierMask
            );

        public Gdk.ModifierType ModifierMask
        {
            get => GetProperty(ModifierMaskProperty);
        }

        #endregion

        #endregion

        #region Methods

        // TODO: Find a solution about closures (#119)
        /*
        public static AccelGroup FromAccelClosure(AccelGroupActivate closure) =>
            WrapHandle<AccelGroup>(Native.from_accel_closure(Marshal.GetFunctionPointerForDelegate(closure)));

        public void Connect
        (
            uint accelKey,
            ModifierType accelMods,
            AccelFlags accelFlags,
            AccelGroupActivate closure
        )
        {
            IntPtr ptr = Marshal.GetFunctionPointerForDelegate(closure);
            Native.connect(Handle, accelKey, accelMods, accelFlags, ptr);
        }

        public void Connect(AccelKey key, AccelGroupActivate closure)
        {
            Connect(key.accel_key, key.accel_mods, (AccelFlags) key.accel_flags, closure);
        }

        public void ConnectByPath(string accelPath, AccelGroupActivate closure)
        {
            IntPtr ptr = Marshal.GetFunctionPointerForDelegate(closure);
            Native.connect_by_path(Handle, accelPath, ptr);
        }

        public bool Disconnect(AccelGroupActivate? closure)
        {
            IntPtr ptr = closure is null ? IntPtr.Zero : Marshal.GetFunctionPointerForDelegate(closure);
            return Native.disconnect(Handle, ptr);
        }
        */

        public bool DisconnectKey(uint accelKey, ModifierType accelMods) =>
            Native.disconnect_key(Handle, accelKey, accelMods);

        public bool DisconnectKey(AccelKey key) =>
            DisconnectKey(key.accel_key, key.accel_mods);

        public bool Activate(uint accelQuark, GObject.Object acceleratable, uint accelKey, Gdk.ModifierType accelMods) =>
            Native.activate(Handle, accelQuark, acceleratable.Handle, accelKey, accelMods);

        public void Lock() => Native.@lock(Handle);

        public void Unlock() => Native.unlock(Handle);

        public bool GetIsLocked() => Native.get_is_locked(Handle);

        public ModifierType GetModifierMask() => Native.get_modifier_mask(Handle);

        public AccelKey Find(AccelGroupFindFunc findFunc) =>
            Marshal.PtrToStructure<AccelKey>(Native.find(Handle, findFunc, IntPtr.Zero));

        public AccelGroupEntry Query(uint accelKey, ModifierType accelMods, out uint entries) =>
            Native.query(Handle, accelKey, accelMods, out entries);

        #endregion
    }
}
