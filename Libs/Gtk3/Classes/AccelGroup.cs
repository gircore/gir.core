using System;
using System.Runtime.InteropServices;
using Gdk;
using GObject;
using Object = GObject.Object;

namespace Gtk
{
    /// <summary>
    /// <para>
    /// A <see cref="AccelGroup" /> represents a group of keyboard accelerators, typically attached to a toplevel
    /// <see cref="Window" /> (with <see cref="Window.AddAccelGroup()" />). Usually you won’t need to create a
    /// <see cref="AccelGroup" /> directly; instead, when using <see cref="UIManager" />, GTK+ automatically sets
    /// up the accelerators for your menus in the ui manager’s <see cref="AccelGroup" />.
    /// </para>
    /// <para>
    /// Note that “accelerators” are different from “mnemonics”. Accelerators are shortcuts for activating a menu item;
    /// they appear alongside the menu item they’re a shortcut for. For example “Ctrl+Q” might appear alongside the
    /// “Quit” menu item. Mnemonics are shortcuts for GUI elements such as text entries or buttons; they appear as
    /// underlined characters. See <see cref="Label.WithMnemonic()" />. Menu items can have both accelerators and
    /// mnemonics, of course.
    /// </para>
    /// </summary>
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

        /// <summary>
        /// Finds the <see cref="AccelGroup"/> to which closure is connected;
        /// see <see cref="Connect(uint,Gdk.ModifierType,Gtk.AccelFlags,Gtk.AccelGroupActivate)"/>.
        /// </summary>
        /// <param name="closure">A closure.</param>
        /// <returns>The <see cref="AccelGroup"/> to which closure is connected, or <c>null</c>.</returns>
        public static AccelGroup FromAccelClosure(AccelGroupActivate closure) =>
            WrapPointerAs<AccelGroup>(Native.from_accel_closure(Marshal.GetFunctionPointerForDelegate(closure)));
        
        /// <summary>
        /// <para>
        /// Installs an accelerator in this group. When this instance is being activated in response to a call
        /// to <see cref="Global.AccelGroupsActivate()"/>, <paramref name="closure"/> will be invoked if the accelKey and accelMods
        /// from <see cref="Global.AccelGroupsActivate()"/> match those of this connection.
        /// </para>
        /// <para>
        /// The signature used for the closure is that of <see cref="AccelGroupActivate"/>.
        /// </para>
        /// <para>
        /// Note that, due to implementation details, a single closure can only be connected to one accelerator group.
        /// </para>
        /// </summary>
        /// <param name="accelKey">Key value of the accelerator.</param>
        /// <param name="accelMods">Modifier combination of the accelerator.</param>
        /// <param name="accelFlags">A flag mask to configure this accelerator.</param>
        /// <param name="closure">Closure to be executed upon accelerator activation.</param>
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

        /// <summary>
        /// <para>
        /// Installs an accelerator in this group. When this instance is being activated in response to a call
        /// to <see cref="Global.AccelGroupsActivate()"/>, <paramref name="closure"/> will be invoked if the accelKey and accelMods
        /// from <see cref="Global.AccelGroupsActivate()"/> match those of this connection.
        /// </para>
        /// <para>
        /// The signature used for the closure is that of <see cref="AccelGroupActivate"/>.
        /// </para>
        /// <para>
        /// Note that, due to implementation details, a single closure can only be connected to one accelerator group.
        /// </para>
        /// </summary>
        /// <param name="key">The accelerator key configuration.</param>
        /// <param name="closure">Closure to be executed upon accelerator activation.</param>
        public void Connect(AccelKey key, AccelGroupActivate closure)
        {
            Connect(key.accel_key, key.accel_mods, (AccelFlags) key.accel_flags, closure);
        }

        /// <summary>
        /// <para>
        /// Installs an accelerator in this group, using an accelerator path to look up the appropriate key and
        /// modifiers (see <see cref="AccelMap.AddEntry()"/>). When this instance is being activated in response to a
        /// call to <see cref="Global.AccelGroupsActivate()"/>, <paramref name="closure"/> will be invoked if the accelKey and accelMods
        /// from <see cref="Global.AccelGroupsActivate()"/> match the key and modifiers for the path.
        /// </para>
        /// <para>
        /// The signature used for the closure is that of <see cref="AccelGroupActivate"/>.
        /// </para>
        /// </summary>
        /// <param name="accelPath">Path used for determining key and modifiers.</param>
        /// <param name="closure">Closure to be executed upon accelerator activation.</param>
        public void ConnectByPath(string accelPath, AccelGroupActivate closure)
        {
            IntPtr ptr = Marshal.GetFunctionPointerForDelegate(closure);
            Native.connect_by_path(Handle, accelPath, ptr);
        }

        /// <summary>
        /// Removes an accelerator previously installed through <see cref="Connect(uint,Gdk.ModifierType,Gtk.AccelFlags,Gtk.AccelGroupActivate)"/>.
        /// </summary>
        /// <param name="closure">The closure to remove from this accelerator group, or <c>null</c> to remove all closures.</param>
        /// <returns><c>true</c> if the closure was found and got disconnected.</returns>
        public bool Disconnect(AccelGroupActivate? closure)
        {
            IntPtr ptr = closure is null ? IntPtr.Zero : Marshal.GetFunctionPointerForDelegate(closure);
            return Native.disconnect(Handle, ptr);
        }

        /// <summary>
        /// Removes an accelerator previously installed through <see cref="Connect(uint,Gdk.ModifierType,Gtk.AccelFlags,Gtk.AccelGroupActivate)"/>.
        /// </summary>
        /// <param name="accelKey">Key value of the accelerator.</param>
        /// <param name="accelMods">Modifier combination of the accelerator.</param>
        /// <returns><c>true</c> if there was an accelerator which could be removed, <c>false</c> otherwise.</returns>
        public bool DisconnectKey(uint accelKey, ModifierType accelMods) =>
            Native.disconnect_key(Handle, accelKey, accelMods);

        /// <summary>
        /// Removes an accelerator previously installed through <see cref="Connect(uint,Gdk.ModifierType,Gtk.AccelFlags,Gtk.AccelGroupActivate)"/>.
        /// </summary>
        /// <param name="key">The accelerator key configuration.</param>
        /// <returns><c>true</c> if there was an accelerator which could be removed, <c>false</c> otherwise.</returns>
        public bool DisconnectKey(AccelKey key) =>
            DisconnectKey(key.accel_key, key.accel_mods);

        /// <summary>
        /// Finds the first accelerator in this instance that matches <paramref name="accelKey"/>
        /// and <paramref name="accelMods"/> , and activates it.
        /// </summary>
        /// <param name="accelQuark">The quark for the accelerator name.</param>
        /// <param name="acceleratable">
        /// The <see cref="Object"/>, usually a <see cref="Window"/>, on which to activate the accelerator.
        /// </param>
        /// <param name="accelKey">Accelerator keyval from a key event.</param>
        /// <param name="accelMods">Keyboard state mask from a key event.</param>
        /// <returns><c>true</c> if an accelerator was activated and handled this keypress.</returns>
        public bool Activate(uint accelQuark, Object acceleratable, uint accelKey, Gdk.ModifierType accelMods) =>
            Native.activate(Handle, accelQuark, GetHandle(acceleratable), accelKey, accelMods);

        /// <summary>
        /// <para>
        /// Locks the given accelerator group.
        /// </para>
        /// <para>
        /// Locking an accelerator group prevents the accelerators contained within it to be changed during runtime.
        /// Refer to <see cref="AccelMap.ChangeEntry()"/> about runtime accelerator changes.
        /// </para>
        /// <para>
        /// If called more than once, this instance remains locked until <see cref="Unlock()"/> has been called
        /// an equivalent number of times.
        /// </para>
        /// </summary>
        public void Lock() => Native.@lock(Handle);

        /// <summary>
        /// Undoes the last call to <see cref="Lock"/> on this <see cref="AccelGroup"/>.
        /// </summary>
        public void Unlock() => Native.unlock(Handle);

        /// <summary>
        /// Locks are added and removed using <see cref="Lock"/> and <see cref="Unlock"/>.
        /// </summary>
        /// <returns><c>true</c> if there are 1 or more locks on this instance, <c>false</c> otherwise.</returns>
        public bool GetIsLocked() => Native.get_is_locked(Handle);

        /// <summary>
        /// Gets a <see cref="ModifierType"/> representing the mask for this instance. For example,
        /// <see cref="ModifierType.ControlMask"/>, <see cref="ModifierType.ShiftMask"/>, etc.
        /// </summary>
        /// <returns>The modifier mask for this accel group.</returns>
        public ModifierType GetModifierMask() => Native.get_modifier_mask(Handle);

        /// <summary>
        /// Finds the first entry in an accelerator group for which <paramref name="findFunc"/> returns <c>true</c>
        /// and returns its <see cref="AccelKey"/>.
        /// </summary>
        /// <param name="findFunc">A function to filter the entries of accel_group with.</param>
        /// <returns>The key of the first entry passing find_func . The key is owned by GTK+ and must not be freed.</returns>
        public AccelKey Find(AccelGroupFindFunc findFunc) =>
            Marshal.PtrToStructure<AccelKey>(Native.find(Handle, findFunc, IntPtr.Zero));

        /// <summary>
        /// Queries an accelerator group for all entries matching <paramref name="accelKey"/>
        /// and <paramref name="accelMods"/>.
        /// </summary>
        /// <param name="accelKey">Key value of the accelerator.</param>
        /// <param name="accelMods">Modifier combination of the accelerator.</param>
        /// <param name="entries">The number of found entries.</param>
        /// <returns></returns>
        public AccelGroupEntry Query(uint accelKey, ModifierType accelMods, ref uint entries) =>
            Native.query(Handle, accelKey, accelMods, ref entries);

        #endregion
    }
}
