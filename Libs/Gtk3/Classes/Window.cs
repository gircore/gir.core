using System;
using System.Runtime.InteropServices;
using GLib;
using GObject;

namespace Gtk
{
    public partial class Window
    {
        #region Properties

        #region AcceptFocusProperty

        /// <summary>
        /// Property descriptor for the <see cref="AcceptFocus"/> property.
        /// </summary>
        public static readonly Property<bool> AcceptFocusProperty = Property<bool>.Wrap<Window>(
            Native.AcceptFocusProperty,
            nameof(AcceptFocus),
            get: (o) => o.AcceptFocus,
            set: (o, v) => o.AcceptFocus = v
        );

        public bool AcceptFocus
        {
            get => GetProperty(AcceptFocusProperty);
            set => SetProperty(AcceptFocusProperty, value);
        }

        #endregion

        #region ApplicationProperty

        /// <summary>
        /// Property descriptor for the <see cref="Application"/> property.
        /// </summary>
        public static readonly Property<Application> ApplicationProperty = Property<Application>.Wrap<Window>(
            Native.ApplicationProperty,
            nameof(Application),
            get: (o) => o.Application,
            set: (o, v) => o.Application = v
        );

        public Application Application
        {
            get => GetProperty(ApplicationProperty);
            set => SetProperty(ApplicationProperty, value);
        }

        #endregion

        #region AttachedToProperty

        /// <summary>
        /// Property descriptor for the <see cref="AttachedTo"/> property.
        /// </summary>
        public static readonly Property<Widget> AttachedToProperty = Property<Widget>.Wrap<Window>(
            Native.AttachedToProperty,
            nameof(AttachedTo),
            get: (o) => o.AttachedTo,
            set: (o, v) => o.AttachedTo = v
        );

        public Widget AttachedTo
        {
            get => GetProperty(AttachedToProperty);
            set => SetProperty(AttachedToProperty, value);
        }

        #endregion

        #region DecoratedProperty

        /// <summary>
        /// Property descriptor for the <see cref="Decorated"/> property.
        /// </summary>
        public static readonly Property<bool> DecoratedProperty = Property<bool>.Wrap<Window>(
            Native.DecoratedProperty,
            nameof(Decorated),
            get: (o) => o.Decorated,
            set: (o, v) => o.Decorated = v
        );

        public bool Decorated
        {
            get => GetProperty(DecoratedProperty);
            set => SetProperty(DecoratedProperty, value);
        }

        #endregion

        #region DefaultHeightProperty

        /// <summary>
        /// Property descriptor for the <see cref="DefaultHeight"/> property.
        /// </summary>
        public static readonly Property<int> DefaultHeightProperty = Property<int>.Wrap<Window>(
            Native.DefaultHeightProperty,
            nameof(DefaultHeight),
            get: (o) => o.DefaultHeight,
            set: (o, v) => o.DefaultHeight = v
        );

        public int DefaultHeight
        {
            get => GetProperty(DefaultHeightProperty);
            set => SetProperty(DefaultHeightProperty, value);
        }

        #endregion

        #region DefaultWidthProperty

        /// <summary>
        /// Property descriptor for the <see cref="DefaultWidth"/> property.
        /// </summary>
        public static readonly Property<int> DefaultWidthProperty = Property<int>.Wrap<Window>(
            Native.DefaultWidthProperty,
            nameof(DefaultWidth),
            get: (o) => o.DefaultWidth,
            set: (o, v) => o.DefaultWidth = v
        );

        public int DefaultWidth
        {
            get => GetProperty(DefaultWidthProperty);
            set => SetProperty(DefaultWidthProperty, value);
        }

        #endregion

        #region DeletableProperty

        /// <summary>
        /// Property descriptor for the <see cref="Deletable"/> property.
        /// </summary>
        public static readonly Property<bool> DeletableProperty = Property<bool>.Wrap<Window>(
            Native.DeletableProperty,
            nameof(Deletable),
            get: (o) => o.Deletable,
            set: (o, v) => o.Deletable = v
        );

        public bool Deletable
        {
            get => GetProperty(DeletableProperty);
            set => SetProperty(DeletableProperty, value);
        }

        #endregion

        #region DestroyWithParentProperty

        /// <summary>
        /// Property descriptor for the <see cref="DestroyWithParent"/> property.
        /// </summary>
        public static readonly Property<bool> DestroyWithParentProperty = Property<bool>.Wrap<Window>(
            Native.DestroyWithParentProperty,
            nameof(DestroyWithParent),
            get: (o) => o.DestroyWithParent,
            set: (o, v) => o.DestroyWithParent = v
        );

        public bool DestroyWithParent
        {
            get => GetProperty(DestroyWithParentProperty);
            set => SetProperty(DestroyWithParentProperty, value);
        }

        #endregion

        #region FocusOnMapProperty

        /// <summary>
        /// Property descriptor for the <see cref="FocusOnMap"/> property.
        /// </summary>
        public static readonly Property<bool> FocusOnMapProperty = Property<bool>.Wrap<Window>(
            Native.FocusOnMapProperty,
            nameof(FocusOnMap),
            get: (o) => o.FocusOnMap,
            set: (o, v) => o.FocusOnMap = v
        );

        public bool FocusOnMap
        {
            get => GetProperty(FocusOnMapProperty);
            set => SetProperty(FocusOnMapProperty, value);
        }

        #endregion

        #region FocusVisibleProperty

        /// <summary>
        /// Property descriptor for the <see cref="FocusVisible"/> property.
        /// </summary>
        public static readonly Property<bool> FocusVisibleProperty = Property<bool>.Wrap<Window>(
            Native.FocusVisibleProperty,
            nameof(FocusVisible),
            get: (o) => o.FocusVisible,
            set: (o, v) => o.FocusVisible = v
        );

        public bool FocusVisible
        {
            get => GetProperty(FocusVisibleProperty);
            set => SetProperty(FocusVisibleProperty, value);
        }

        #endregion

        #region GravityProperty

        /// <summary>
        /// Property descriptor for the <see cref="Gravity"/> property.
        /// </summary>
        public static readonly Property<Gdk.Gravity> GravityProperty = Property<Gdk.Gravity>.Wrap<Window>(
            Native.GravityProperty,
            nameof(Gravity),
            get: (o) => o.Gravity,
            set: (o, v) => o.Gravity = v
        );

        public Gdk.Gravity Gravity
        {
            get => GetProperty(GravityProperty);
            set => SetProperty(GravityProperty, value);
        }

        #endregion

        #region HasResizeGripProperty

        /// <summary>
        /// Property descriptor for the <see cref="HasResizeGrip"/> property.
        /// </summary>
        [Obsolete("Resize grips have been removed.")]
        public static readonly Property<bool> HasResizeGripProperty = Property<bool>.Wrap<Window>(
            Native.HasResizeGripProperty,
            nameof(HasResizeGrip),
            get: (o) => o.HasResizeGrip,
            set: (o, v) => o.HasResizeGrip = v
        );

        [Obsolete("Resize grips have been removed.")]
        public bool HasResizeGrip
        {
            get => GetProperty(HasResizeGripProperty);
            set => SetProperty(HasResizeGripProperty, value);
        }

        #endregion

        #region HasToplevelFocusProperty

        /// <summary>
        /// Property descriptor for the <see cref="HasToplevelFocus"/> property.
        /// </summary>
        public static readonly Property<bool> HasToplevelFocusProperty = Property<bool>.Wrap<Window>(
            Native.HasToplevelFocusProperty,
            nameof(HasToplevelFocus),
            get: (o) => o.HasToplevelFocus
        );

        public bool HasToplevelFocus
        {
            get => GetProperty(HasToplevelFocusProperty);
        }

        #endregion

        #region HideTitlebarWhenMaximizedProperty

        /// <summary>
        /// Property descriptor for the <see cref="HideTitlebarWhenMaximized"/> property.
        /// </summary>
        public static readonly Property<bool> HideTitlebarWhenMaximizedProperty = Property<bool>.Wrap<Window>(
            Native.HideTitlebarWhenMaximizedProperty,
            nameof(HideTitlebarWhenMaximized),
            get: (o) => o.HideTitlebarWhenMaximized,
            set: (o, v) => o.HideTitlebarWhenMaximized = v
        );

        public bool HideTitlebarWhenMaximized
        {
            get => GetProperty(HideTitlebarWhenMaximizedProperty);
            set => SetProperty(HideTitlebarWhenMaximizedProperty, value);
        }

        #endregion

        #region IconProperty

        /// <summary>
        /// Property descriptor for the <see cref="Icon"/> property.
        /// </summary>
        public static readonly Property<GdkPixbuf.Pixbuf> IconProperty = Property<GdkPixbuf.Pixbuf>.Wrap<Window>(
            Native.IconProperty,
            nameof(Icon),
            get: (o) => o.Icon,
            set: (o, v) => o.Icon = v
        );

        public GdkPixbuf.Pixbuf Icon
        {
            get => GetProperty(IconProperty);
            set => SetProperty(IconProperty, value);
        }

        #endregion

        #region IconNameProperty

        /// <summary>
        /// Property descriptor for the <see cref="IconName"/> property.
        /// </summary>
        public static readonly Property<string> IconNameProperty = Property<string>.Wrap<Window>(
            Native.IconNameProperty,
            nameof(IconName),
            get: (o) => o.IconName,
            set: (o, v) => o.IconName = v
        );

        public string IconName
        {
            get => GetProperty(IconNameProperty);
            set => SetProperty(IconNameProperty, value);
        }

        #endregion

        #region IsActiveProperty

        /// <summary>
        /// Property descriptor for the <see cref="IsActive"/> property.
        /// </summary>
        public static readonly Property<bool> IsActiveProperty = Property<bool>.Wrap<Window>(
            Native.IsActiveProperty,
            nameof(IsActive),
            get: (o) => o.IsActive
        );

        public bool IsActive
        {
            get => GetProperty(IsActiveProperty);
        }

        #endregion

        #region IsMaximizedProperty

        /// <summary>
        /// Property descriptor for the <see cref="IsMaximized"/> property.
        /// </summary>
        public static readonly Property<bool> IsMaximizedProperty = Property<bool>.Wrap<Window>(
            Native.IsMaximizedProperty,
            nameof(IsMaximized),
            get: (o) => o.IsMaximized
        );

        public bool IsMaximized
        {
            get => GetProperty(IsMaximizedProperty);
        }

        #endregion

        #region MnemonicsVisibleProperty

        /// <summary>
        /// Property descriptor for the <see cref="MnemonicsVisible"/> property.
        /// </summary>
        public static readonly Property<bool> MnemonicsVisibleProperty = Property<bool>.Wrap<Window>(
            Native.MnemonicsVisibleProperty,
            nameof(MnemonicsVisible),
            get: (o) => o.MnemonicsVisible,
            set: (o, v) => o.MnemonicsVisible = v
        );

        public bool MnemonicsVisible
        {
            get => GetProperty(MnemonicsVisibleProperty);
            set => SetProperty(MnemonicsVisibleProperty, value);
        }

        #endregion

        #region ModalProperty

        /// <summary>
        /// Property descriptor for the <see cref="Modal"/> property.
        /// </summary>
        public static readonly Property<bool> ModalProperty = Property<bool>.Wrap<Window>(
            Native.ModalProperty,
            nameof(Modal),
            get: (o) => o.Modal,
            set: (o, v) => o.Modal = v
        );

        public bool Modal
        {
            get => GetProperty(ModalProperty);
            set => SetProperty(ModalProperty, value);
        }

        #endregion

        #region ResizableProperty

        /// <summary>
        /// Property descriptor for the <see cref="Resizable"/> property.
        /// </summary>
        public static readonly Property<bool> ResizableProperty = Property<bool>.Wrap<Window>(
            Native.ResizableProperty,
            nameof(Resizable),
            get: (o) => o.Resizable,
            set: (o, v) => o.Resizable = v
        );

        public bool Resizable
        {
            get => GetProperty(ResizableProperty);
            set => SetProperty(ResizableProperty, value);
        }

        #endregion

        #region ResizeGripVisibleProperty

        /// <summary>
        /// Property descriptor for the <see cref="ResizeGripVisible"/> property.
        /// </summary>
        [Obsolete("Resize grips have been removed.")]
        public static readonly Property<bool> ResizeGripVisibleProperty = Property<bool>.Wrap<Window>(
            Native.ResizeGripVisibleProperty,
            nameof(ResizeGripVisible),
            get: (o) => o.ResizeGripVisible
        );

        [Obsolete("Resize grips have been removed.")]
        public bool ResizeGripVisible
        {
            get => GetProperty(ResizeGripVisibleProperty);
        }

        #endregion

        #region RoleProperty

        /// <summary>
        /// Property descriptor for the <see cref="Role"/> property.
        /// </summary>
        public static readonly Property<string> RoleProperty = Property<string>.Wrap<Window>(
            Native.RoleProperty,
            nameof(Role),
            get: (o) => o.Role,
            set: (o, v) => o.Role = v
        );

        public string Role
        {
            get => GetProperty(RoleProperty);
            set => SetProperty(RoleProperty, value);
        }

        #endregion

        #region ScreenProperty

        /// <summary>
        /// Property descriptor for the <see cref="Screen"/> property.
        /// </summary>
        public static readonly Property<Gdk.Screen> ScreenProperty = Property<Gdk.Screen>.Wrap<Window>(
            Native.ScreenProperty,
            nameof(Screen),
            get: (o) => o.Screen,
            set: (o, v) => o.Screen = v
        );

        public Gdk.Screen Screen
        {
            get => GetProperty(ScreenProperty);
            set => SetProperty(ScreenProperty, value);
        }

        #endregion

        #region SkipPagerHintProperty

        /// <summary>
        /// Property descriptor for the <see cref="SkipPagerHint"/> property.
        /// </summary>
        public static readonly Property<bool> SkipPagerHintProperty = Property<bool>.Wrap<Window>(
            Native.SkipPagerHintProperty,
            nameof(SkipPagerHint),
            get: (o) => o.SkipPagerHint,
            set: (o, v) => o.SkipPagerHint = v
        );

        public bool SkipPagerHint
        {
            get => GetProperty(SkipPagerHintProperty);
            set => SetProperty(SkipPagerHintProperty, value);
        }

        #endregion

        #region SkipTaskbarHintProperty

        /// <summary>
        /// Property descriptor for the <see cref="SkipTaskbarHint"/> property.
        /// </summary>
        public static readonly Property<bool> SkipTaskbarHintProperty = Property<bool>.Wrap<Window>(
            Native.SkipTaskbarHintProperty,
            nameof(SkipTaskbarHint),
            get: (o) => o.SkipTaskbarHint,
            set: (o, v) => o.SkipTaskbarHint = v
        );

        public bool SkipTaskbarHint
        {
            get => GetProperty(SkipTaskbarHintProperty);
            set => SetProperty(SkipTaskbarHintProperty, value);
        }

        #endregion

        #region StartupIdProperty

        /// <summary>
        /// Property descriptor for the <see cref="StartupId"/> property.
        /// </summary>
        public static readonly Property<string> StartupIdProperty = Property<string>.Wrap<Window>(
            Native.StartupIdProperty,
            nameof(StartupId),
            set: (o, v) => o.StartupId = v
        );

        public string StartupId
        {
            set => SetProperty(StartupIdProperty, value);
        }

        #endregion

        #region TitleProperty

        /// <summary>
        /// Property descriptor for the <see cref="Title"/> property.
        /// </summary>
        public static readonly Property<string> TitleProperty = Property<string>.Wrap<Window>(
            Native.TitleProperty,
            nameof(Title),
            get: (o) => o.Title,
            set: (o, v) => o.Title = v
        );

        public string Title
        {
            get => GetProperty(TitleProperty);
            set => SetProperty(TitleProperty, value);
        }

        #endregion

        #region TransientForProperty

        /// <summary>
        /// Property descriptor for the <see cref="TransientFor"/> property.
        /// </summary>
        public static readonly Property<ulong> TransientForProperty = Property<ulong>.Wrap<Window>(
            Native.TransientForProperty,
            nameof(TransientFor),
            get: (o) => o.TransientFor,
            set: (o, v) => o.TransientFor = v
        );

        public ulong TransientFor
        {
            get => GetProperty(TransientForProperty);
            set => SetProperty(TransientForProperty, value);
        }

        #endregion

        #region TypeProperty

        /// <summary>
        /// Property descriptor for the <see cref="Type"/> property.
        /// </summary>
        public static readonly Property<WindowType> TypeProperty = Property<WindowType>.Wrap<Window>(
            Native.TypeProperty,
            nameof(Type),
            get: (o) => o.Type,
            set: (o, v) => o.Type = v
        );

        public WindowType Type
        {
            get => GetProperty(TypeProperty);
            set => SetProperty(TypeProperty, value);
        }

        #endregion

        #region TypeHintProperty

        /// <summary>
        /// Property descriptor for the <see cref="TypeHint"/> property.
        /// </summary>
        public static readonly Property<Gdk.WindowTypeHint> TypeHintProperty =
            Property<Gdk.WindowTypeHint>.Wrap<Window>(
                Native.TypeHintProperty,
                nameof(TypeHint),
                get: (o) => o.TypeHint,
                set: (o, v) => o.TypeHint = v
            );

        public Gdk.WindowTypeHint TypeHint
        {
            get => GetProperty(TypeHintProperty);
            set => SetProperty(TypeHintProperty, value);
        }

        #endregion

        #region UrgencyHintProperty

        /// <summary>
        /// Property descriptor for the <see cref="UrgencyHint"/> property.
        /// </summary>
        public static readonly Property<bool> UrgencyHintProperty = Property<bool>.Wrap<Window>(
            Native.UrgencyHintProperty,
            nameof(UrgencyHint),
            get: (o) => o.UrgencyHint,
            set: (o, v) => o.UrgencyHint = v
        );

        public bool UrgencyHint
        {
            get => GetProperty(UrgencyHintProperty);
            set => SetProperty(UrgencyHintProperty, value);
        }

        #endregion

        #region WindowPositionProperty

        /// <summary>
        /// Property descriptor for the <see cref="WindowPosition"/> property.
        /// </summary>
        public static readonly Property<WindowPosition> WindowPositionProperty = Property<WindowPosition>.Wrap<Window>(
            Native.WindowPositionProperty,
            nameof(WindowPosition),
            get: (o) => o.WindowPosition,
            set: (o, v) => o.WindowPosition = v
        );

        public WindowPosition WindowPosition
        {
            get => GetProperty(WindowPositionProperty);
            set => SetProperty(WindowPositionProperty, value);
        }

        #endregion

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new instance of <see cref="Window"/>.
        /// </summary>
        /// <param name="type">The type of window.</param>
        public Window(WindowType type)
            : this(ConstructParameter.With(TypeProperty, type))
        {
        }

        /// <summary>
        /// Creates a new instance of <see cref="Window"/>.
        /// </summary>
        /// <param name="title">The window title.</param>
        public Window(string title)
            : this(ConstructParameter.With(TitleProperty, title))
        {
        }

        #endregion

        #region Methods

        public void SetTitle(string title) =>
            Native.set_title(Handle, title);

        [Obsolete]
        public void SetWMClass(string name, string @class) =>
            Native.set_wmclass(Handle, name, @class);

        public void SetResizable(bool resizable) =>
            Native.set_resizable(Handle, resizable);

        public bool GetResizable() =>
            Native.get_resizable(Handle);

        public void AddAccelGroup(AccelGroup accelGroup) =>
            Native.add_accel_group(Handle, accelGroup.Handle);

        public void RemoveAccelGroup(AccelGroup accelGroup) =>
            Native.remove_accel_group(Handle, accelGroup.Handle);

        public bool ActivateFocus() =>
            Native.activate_focus(Handle);

        public bool ActivateDefault() =>
            Native.activate_default(Handle);

        public void SetModal(bool modal) =>
            Native.set_modal(Handle, modal);

        public void SetDefaultSize(int width, int height) =>
            Native.set_default_size(Handle, width, height);

        [Obsolete("This function does nothing. If you want to set a default size, use SetDefaultSize() instead.")]
        public void SetDefaultGeometry(int width, int height) =>
            Native.set_default_geometry(Handle, width, height);

        public void SetGeometryHints(Widget geometryWidget, Gdk.Geometry geometry, Gdk.WindowHints geometryMask)
        {
            MarshalHelper.ToPtrAndFree(geometry, (geoPtr)
                => Native.set_geometry_hints(Handle, geometryWidget.Handle, geoPtr, geometryMask)
            );
        }

        public void SetGravity(Gdk.Gravity gravity) =>
            Native.set_gravity(Handle, gravity);

        public Gdk.Gravity GetGravity() =>
            Native.get_gravity(Handle);

        public void SetPosition(WindowPosition position) =>
            Native.set_position(Handle, position);

        public void SetTransientFor(Window? parent) =>
            Native.set_transient_for(Handle, parent is null ? IntPtr.Zero : parent.Handle);

        public void SetAttachedTo(Widget? attachWidget) =>
            Native.set_attached_to(Handle, attachWidget is null ? IntPtr.Zero : attachWidget.Handle);

        public void SetDestroyWithParent(bool setting) =>
            Native.set_destroy_with_parent(Handle, setting);

        public void SetHideTitlebarWhenMaximized(bool setting) =>
            Native.set_hide_titlebar_when_maximized(Handle, setting);

        public void SetScreen(Gdk.Screen screen) =>
            Native.set_screen(Handle, screen.Handle);

        public new Gdk.Screen GetScreen() =>
            WrapHandle<Gdk.Screen>(Native.get_screen(Handle), false);

        public GLib.List ListToplevels()
        {
            IntPtr listPtr = Native.list_toplevels();
            return Marshal.PtrToStructure<GLib.List>(listPtr);
        }

        public void AddMnemonic(uint keyVal, Widget target) =>
            Native.add_mnemonic(Handle, keyVal, target.Handle);

        public void RemoveMnemonic(uint keyVal, Widget target) =>
            Native.remove_mnemonic(Handle, keyVal, target.Handle);

        public bool MnemonicActivate(uint keyVal, Gdk.ModifierType modifierType) =>
            Native.mnemonic_activate(Handle, keyVal, modifierType);

        public bool ActivateKey(Gdk.EventKey @event)
        {
            return MarshalHelper.ToPtrAndFree(@event, (eventPtr) 
                => Native.activate_key(Handle, eventPtr)
            );
        }

        public bool PropagateKeyEvent(Gdk.EventKey @event)
        {
            return MarshalHelper.ToPtrAndFree(@event, (eventPtr)
                => Native.propagate_key_event(Handle, eventPtr)
            );
        }

        public Widget? GetFocus() =>
            GetFocus<Widget>();

        public T? GetFocus<T>() where T : Widget =>
            WrapNullableHandle<T>(Native.get_focus(Handle), false);

        public void SetFocus(Widget? focus) =>
            Native.set_focus(Handle, focus is null ? IntPtr.Zero : focus.Handle);

        public void SetDefault(Widget? defaultWidget) =>
            Native.set_default(Handle, defaultWidget is null ? IntPtr.Zero : defaultWidget.Handle);

        public void Present() =>
            Native.present(Handle);

        public void PresentWithTime(uint timestamp) =>
            Native.present_with_time(Handle, timestamp);

        public void Close() =>
            Native.close(Handle);

        public void Iconify() =>
            Native.iconify(Handle);

        public void Deiconify() =>
            Native.deiconify(Handle);

        public void Stick() =>
            Native.stick(Handle);

        public void Unstick() =>
            Native.unstick(Handle);

        public void Maximize() =>
            Native.maximize(Handle);

        public void Unmaximize() =>
            Native.unmaximize(Handle);

        public void Fullscreen() =>
            Native.fullscreen(Handle);

        public void FullscreenOnMonitor(Gdk.Screen screen, int monitor) =>
            Native.fullscreen_on_monitor(Handle, screen.Handle, monitor);

        public void Unfullscreen() =>
            Native.unfullscreen(Handle);

        public void SetKeepAbove(bool setting) =>
            Native.set_keep_above(Handle, setting);

        public void SetKeepBelow(bool setting) =>
            Native.set_keep_below(Handle, setting);

        public void BeginResizeDrag(Gdk.WindowEdge edge, int button, int rootX, int rootY, uint timestamp) =>
            Native.begin_resize_drag(Handle, edge, button, rootX, rootY, timestamp);

        public void BeginMoveDrag(int button, int rootX, int rootY, uint timestamp) =>
            Native.begin_move_drag(Handle, button, rootX, rootY, timestamp);

        public void SetDecorated(bool setting) =>
            Native.set_decorated(Handle, setting);

        public void SetDeletable(bool setting) =>
            Native.set_deletable(Handle, setting);

        public void SetMnemonicModifier(Gdk.ModifierType modifier) =>
            Native.set_mnemonic_modifier(Handle, modifier);

        public void SetTypeHint(Gdk.WindowTypeHint hint) =>
            Native.set_type_hint(Handle, hint);

        public void SetSkipTaskbarHint(bool setting) =>
            Native.set_skip_taskbar_hint(Handle, setting);

        public void SetSkipPagerHint(bool setting) =>
            Native.set_skip_pager_hint(Handle, setting);

        public void SetUrgencyHint(bool setting) =>
            Native.set_urgency_hint(Handle, setting);

        public void SetAcceptFocus(bool setting) =>
            Native.set_accept_focus(Handle, setting);

        public void SetFocusOnMap(bool setting) =>
            Native.set_focus_on_map(Handle, setting);

        public void SetStartupId(string id) =>
            Native.set_startup_id(Handle, id);

        public void SetRole(string role) =>
            Native.set_role(Handle, role);

        public bool GetDecorated() =>
            Native.get_decorated(Handle);

        public bool GetDeletable() =>
            Native.get_deletable(Handle);

        public static GLib.List GetDefaultIconList()
        {
            IntPtr listPtr = Native.get_default_icon_list();
            return Marshal.PtrToStructure<GLib.List>(listPtr);
        }

        public static string? GetDefaultIconName() =>
            Marshal.PtrToStringAnsi(Native.get_default_icon_name());

        public void GetDefaultSize(out int width, out int height)
        {
            int w = 0, h = 0;
            Native.get_default_size(Handle, out w, out h);
            width = w;
            height = h;
        }

        public bool GetDestroyWithParent() =>
            Native.get_destroy_with_parent(Handle);

        public bool GetHideTitlebarWhenMaximized() =>
            Native.get_hide_titlebar_when_maximized(Handle);

        public GdkPixbuf.Pixbuf? GetIcon() =>
            WrapNullableHandle<GdkPixbuf.Pixbuf>(Native.get_icon(Handle),false);

        public GLib.List GetIconList()
        {
            IntPtr listPtr = Native.get_icon_list(Handle);
            return Marshal.PtrToStructure<GLib.List>(listPtr);
        }

        public string? GetIconName() =>
            Marshal.PtrToStringAnsi(Native.get_icon_name(Handle));

        public Gdk.ModifierType GetMnemonicModifier() =>
            Native.get_mnemonic_modifier(Handle);

        public bool GetModal() =>
            Native.get_modal(Handle);

        public void GetPosition(out int rootX, out int rootY)
        {
            int x = 0, y = 0;
            Native.get_position(Handle, out x, out y);
            rootX = x;
            rootY = y;
        }

        public string? GetRole() =>
            Marshal.PtrToStringAnsi(Native.get_role(Handle));

        public void GetSize(out int width, out int height)
        {
            int w = 0, h = 0;
            Native.get_size(Handle, out w, out h);
            width = w;
            height = h;
        }

        public string? GetTitle() =>
            Marshal.PtrToStringAnsi(Native.get_title(Handle));

        public Window? GetTransientFor()
        {
            return WrapNullableHandle<Window>(Native.get_transient_for(Handle), false);
        }

        public Widget? GetAttachedTo()
        {
            return WrapNullableHandle<Widget>(Native.get_attached_to(Handle), false);
        }

        public Gdk.WindowTypeHint GetTypeHint() =>
            Native.get_type_hint(Handle);

        public bool GetSkipTaskbarHint() =>
            Native.get_skip_taskbar_hint(Handle);

        public bool GetSkipPagerHint() =>
            Native.get_skip_pager_hint(Handle);

        public bool GetUrgencyHint() =>
            Native.get_urgency_hint(Handle);

        public bool GetAcceptFocus() =>
            Native.get_accept_focus(Handle);

        public WindowType GetWindowType() =>
            Native.get_window_type(Handle);

        public void Move(int x, int y) =>
            Native.move(Handle, x, y);

        [Obsolete("Geometry handling in GTK is deprecated.")]
        public bool ParseGeometry(string geometry) =>
            Native.parse_geometry(Handle, geometry);

        [Obsolete("GUI builders can call Hide(), Unrealize() and then Show() on window themselves, if they still need this functionality.")]
        public void ReshowWithInitialSize() =>
            Native.reshow_with_initial_size(Handle);

        public void Resize(int width, int height) =>
            Native.resize(Handle, width, height);

        [Obsolete("This function does nothing. Use Window.Resize() and compute the geometry yourself.")]
        public void ResizeToGeometry(int width, int height) =>
            Native.resize_to_geometry(Handle, width, height);

        public static void SetDefaultIconList(GLib.List list)
        {
            MarshalHelper.ToPtrAndFree(list, Native.set_default_icon_list);
        }

        public static void SetDefaultIcon(GdkPixbuf.Pixbuf icon) =>
            Native.set_default_icon(icon.Handle);

        public static bool SetDefaultIconFromFile(string filename)
        {
            var result = Native.set_default_icon_from_file(filename, out IntPtr e);
            GLib.Error.ThrowOnError(e);

            return result;
        }

        public static void SetDefaultIconName(string name) =>
            Native.set_default_icon_name(name);

        public void SetIcon(GdkPixbuf.Pixbuf? icon) =>
            Native.set_icon(Handle, icon is null ? IntPtr.Zero : icon.Handle);

        public void SetIconList(GLib.List list)
        {
            MarshalHelper.ToPtrAndFree(list, (listPtr)
                => Native.set_icon_list(Handle, listPtr)
            );
        }

        public bool SetIconFromFile(string filename)
        {
            var result = Native.set_icon_from_file(Handle, filename, out IntPtr e);
            GLib.Error.ThrowOnError(e);

            return result;
        }

        public void SetIconName(string name) =>
            Native.set_icon_name(Handle, name);

        public static void SetAutoStartupNotification(bool setting) =>
            Native.set_auto_startup_notification(setting);

        [Obsolete("Use Widget.GetOpacity() instead.")]
        public double GetOpacity() =>
            Native.get_opacity(Handle);

        [Obsolete("Use Widget.SetOpacity() instead.")]
        public void SetOpacity(double opacity) =>
            Native.set_opacity(Handle, opacity);

        public bool GetMnemonicsVisible() =>
            Native.get_mnemonics_visible(Handle);

        public void SetMnemonicsVisible(bool setting) =>
            Native.set_mnemonics_visible(Handle, setting);

        public bool GetFocusVisible() =>
            Native.get_focus_visible(Handle);

        public void SetFocusVisible(bool setting) =>
            Native.set_focus_visible(Handle, setting);

        [Obsolete("Resize grips have been removed.")]
        public bool GetHasResizeGrip() =>
            Native.get_has_resize_grip(Handle);

        [Obsolete("Resize grips have been removed.")]
        public void SetHasResizeGrip(bool setting) =>
            Native.set_has_resize_grip(Handle, setting);

        [Obsolete("Resize grips have been removed.")]
        public bool ResizeGripIsVisible() =>
            Native.resize_grip_is_visible(Handle);

        [Obsolete("Resize grips have been removed.")]
        public bool GetResizeGripArea(out Gdk.Rectangle rect)
        {
            IntPtr rectPtr = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(Gdk.Rectangle)));
            
            bool result;
            try
            {
                result = Native.get_resize_grip_area(Handle, ref rectPtr);
                rect = Marshal.PtrToStructure<Gdk.Rectangle>(rectPtr);
            }
            finally
            {
                Marshal.FreeHGlobal(rectPtr);   
            }
            return result;
        }

        public Application? GetApplication() =>
            WrapNullableHandle<Application>(Native.get_application(Handle), false);

        public void SetApplication(Application? application) =>
            Native.set_application(Handle, application is null ? IntPtr.Zero : application.Handle);

        public void SetHasUserRefCount(bool setting) =>
            Native.set_has_user_ref_count(Handle, setting);

        public void SetTitlebar(Widget? titlebar) =>
            Native.set_titlebar(Handle, titlebar is null ? IntPtr.Zero : titlebar.Handle);

        public Widget? GetTitlebar() =>
            WrapNullableHandle<Widget>(Native.get_titlebar(Handle), false);

        public static void SetInteractiveDebugging(bool enable) =>
            Native.set_interactive_debugging(enable);

        #endregion
    }
}
