using System;
using System.Runtime.InteropServices;
using GObject;

#pragma warning disable 618

namespace Gtk
{
    /// <summary>
    /// A <see cref="Window"/> is a toplevel window which can contain other widgets. Windows normally have decorations
    /// that are under the control of the windowing system and allow the user to manipulate the window
    /// (resize it, move it, close it,...).
    /// </summary>
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

        /// <summary>
        /// Whether the window should receive the input focus.
        /// </summary>
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

        /// <summary>
        /// <para>
        /// The <see cref="Application"/> associated with the <see cref="Window"/>.
        /// </para>
        /// <para>
        /// The application will be kept alive for at least as long as it
        /// has any windows associated with it (see <see cref="GLib.Global.ApplicationHold()"/>
        /// for a way to keep it alive without windows).
        /// </para>
        /// <para>
        /// Normally, the connection between the application and the window
        /// will remain until the window is destroyed, but you can explicitly
        /// remove it by setting the <see cref="Application"/> property to <c>null</c>.
        /// </para>
        /// </summary>
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

        /// <summary>
        /// <para>
        /// The widget to which this <see cref="Window"/> is attached.
        /// See <see cref="SetAttachedTo()"/>.
        /// </para>
        /// <para>
        /// Examples of places where specifying this relation is useful are
        /// for instance a <see cref="Menu"/> created by a <see cref="ComboBox"/>, a completion
        /// popup window created by <see cref="Entry"/> or a typeahead search entry
        /// created by <see cref="TreeView"/>.
        /// </para>
        /// </summary>
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

        /// <summary>
        /// Whether the window should be decorated by the window manager.
        /// </summary>
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

        /// <summary>
        /// The default height of the window, used when initially showing the window.
        /// </summary>
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

        /// <summary>
        /// The default width of the window, used when initially showing the window.
        /// </summary>
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

        /// <summary>
        /// Whether the window frame should have a close button.
        /// </summary>
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

        /// <summary>
        /// If this window should be destroyed when the parent is destroyed.
        /// </summary>
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

        /// <summary>
        /// Whether the window should receive the input focus when mapped.
        /// </summary>
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

        /// <summary>
        /// <para>
        /// Whether 'focus rectangles' are currently visible in this window.
        /// </para>
        /// <para>
        /// This property is maintained by GTK+ based on user input
        /// and should not be set by applications.
        /// </para>
        /// </summary>
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

        /// <summary>
        /// The window gravity of the window. <see cref="Move()"/> and <see cref="Gdk.Gravity"/> for
        /// more details about window gravity.
        /// </summary>
        public Gdk.Gravity Gravity
        {
            get => GetProperty(GravityProperty);
            set => SetProperty(GravityProperty, value);
        }

        #endregion

        #region HasResizeGripProperty

        /// <summary>
        /// Property descriptor for the <see cref="HasResizeGrip"/> property.
        /// </summary>[Obsolete("Resize grips have been removed.")]
        public static readonly Property<bool> HasResizeGripProperty = Property<bool>.Wrap<Window>(
            Native.HasResizeGripProperty,
            nameof(HasResizeGrip),
            get: (o) => o.HasResizeGrip,
            set: (o, v) => o.HasResizeGrip = v
        );

        /// <summary>
        /// <para>
        /// Whether the window has a corner resize grip.
        /// </para>
        /// <para>
        /// Note that the resize grip is only shown if the window is
        /// actually resizable and not maximized. Use <see cref="ResizeGripVisible"/>
        /// to find out if the resize grip is currently shown.
        /// </para>
        /// </summary>
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

        /// <summary>
        /// Whether the input focus is within this <see cref="Window"/>.
        /// </summary>
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

        /// <summary>
        /// Whether the titlebar should be hidden during maximization.
        /// </summary>
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

        /// <summary>
        /// Icon for this <see cref="Window"/>.
        /// </summary>
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

        /// <summary>
        /// The <see cref="IconName"/> property specifies the name of the themed icon to
        /// use as the window icon. See <see cref="IconTheme"/> for more details.
        /// </summary>
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

        /// <summary>
        /// Whether the toplevel is the current active window.
        /// </summary>
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

        /// <summary>
        /// Whether the window is maximized.
        /// </summary>
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

        /// <summary>
        /// <para>
        /// Whether mnemonics are currently visible in this window.
        /// </para>
        /// <para>
        /// This property is maintained by GTK+ based on user input,
        /// and should not be set by applications.
        /// </para>
        /// </summary>
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

        /// <summary>
        /// If <c>true</c>, the window is modal (other windows are not usable while this one is up).
        /// </summary>
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

        /// <summary>
        /// If <c>true</c>, users can resize the <see cref="Window"/>.
        /// </summary>
        public bool Resizable
        {
            get => GetProperty(ResizableProperty);
            set => SetProperty(ResizableProperty, value);
        }

        #endregion

        #region ResizeGripVisibleProperty

        /// <summary>
        /// Property descriptor for the <see cref="ResizeGripVisible"/> property.
        /// </summary>[Obsolete("Resize grips have been removed.")]
        public static readonly Property<bool> ResizeGripVisibleProperty = Property<bool>.Wrap<Window>(
            Native.ResizeGripVisibleProperty,
            nameof(ResizeGripVisible),
            get: (o) => o.ResizeGripVisible
        );

        /// <summary>
        /// Whether a corner resize grip is currently shown.
        /// </summary>
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

        /// <summary>
        /// Unique identifier for the window to be used when restoring a session.
        /// </summary>
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

        /// <summary>
        /// The <see cref="Gdk.Screen"/> where this window will be displayed.
        /// </summary>
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

        /// <summary>
        /// <c>true</c> if the window should not be in the pager.
        /// </summary>
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

        /// <summary>
        /// <c>true</c> if the window should not be in the task bar.
        /// </summary>
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

        /// <summary>
        /// The <see cref="StartupId"/> is a write-only property for setting window's
        /// startup notification identifier. See <see cref="SetStartupId()"/> for more details.
        /// </summary>
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

        /// <summary>
        /// The title of the window.
        /// </summary>
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

        /// <summary>
        /// The transient parent of the window. See <see cref="SetTransientFor()"/> for
        /// more details about transient windows.
        /// </summary>
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

        /// <summary>
        /// The type of the window.
        /// </summary>
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

        /// <summary>
        /// Hint to help the desktop environment understand what kind of window this is and how to treat it.
        /// </summary>
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

        /// <summary>
        /// <c>true</c> if the window should be brought to the user's attention.
        /// </summary>
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

        /// <summary>
        /// The initial position of the window.
        /// </summary>
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

        /// <summary>
        /// Sets the title of the <see cref="Window"/>. The title of a window will be displayed in its title bar;
        /// on the X Window System, the title bar is rendered by the window manager, so exactly how the title
        /// appears to users may vary according to a user’s exact configuration. The title should help a user
        /// distinguish this window from other windows they may have open. A good title might include the application
        /// name and current document filename, for example.
        /// </summary>
        /// <param name="title">Title of the window</param>
        public void SetTitle(string title) =>
            Native.set_title(Handle, title);

        /// <summary>
        /// Don’t use this function. It sets the X Window System “class” and “name” hints for a window.
        /// According to the ICCCM, you should always set these to the same value for all windows in an application,
        /// and GTK+ sets them to that value by default, so calling this function is sort of pointless.
        /// However, you may want to call <see cref="SetRole"/> on each window in your application, for the benefit
        /// of the session manager. Setting the role allows the window manager to restore window positions when
        /// loading a saved session.
        /// </summary>
        /// <param name="name">Window name hint.</param>
        /// <param name="class">Window class hint.</param>
        [Obsolete]
        public void SetWMClass(string name, string @class) =>
            Native.set_wmclass(Handle, name, @class);

        /// <summary>
        /// Sets whether the user can resize a window. Windows are user resizable by default.
        /// </summary>
        /// <param name="resizable"><c>true</c> if the user can resize this <see cref="Window"/>.</param>
        public void SetResizable(bool resizable) =>
            Native.set_resizable(Handle, resizable);

        /// <summary>
        /// Gets the value set by <see cref="SetResizable"/>.
        /// </summary>
        /// <returns><c>true</c> if the user can resize this <see cref="Window"/>.</returns>
        public bool GetResizable() =>
            Native.get_resizable(Handle);

        /// <summary>
        /// Associate <paramref name="accelGroup"/> with <see cref="Window"/>, such that calling
        /// <see cref="Global.AccelGroupsActivate"/> on window will activate accelerators in <paramref name="accelGroup"/>.
        /// </summary>
        /// <param name="accelGroup">A <see cref="AccelGroup"/>.</param>
        public void AddAccelGroup(AccelGroup accelGroup) =>
            Native.add_accel_group(Handle, GetHandle(accelGroup));

        /// <summary>
        /// Reverses the effects of <see cref="AddAccelGroup"/>.
        /// </summary>
        /// <param name="accelGroup">A <see cref="AccelGroup"/>.</param>
        public void RemoveAccelGroup(AccelGroup accelGroup) =>
            Native.remove_accel_group(Handle, GetHandle(accelGroup));

        /// <summary>
        /// Activates the current focused widget within the window.
        /// </summary>
        /// <returns><c>true</c> if a widget got activated.</returns>
        public bool ActivateFocus() =>
            Native.activate_focus(Handle);

        /// <summary>
        /// Activates the default widget for the window, unless the current focused widget has been configured to
        /// receive the default action (see <see cref="Widget.SetReceivesDefault"/>), in which case the focused widget is activated.
        /// </summary>
        /// <returns><c>true</c> if a widget got activated.</returns>
        public bool ActivateDefault() =>
            Native.activate_default(Handle);

        /// <summary>
        /// Sets a window modal or non-modal. Modal windows prevent interaction with other windows in the same
        /// application. To keep modal dialogs on top of main application windows, use <see cref="SetTransientFor"/>
        /// to make the dialog transient for the parent; most window managers will then disallow lowering the dialog
        /// below the parent.
        /// </summary>
        /// <param name="modal">Whether the window is modal.</param>
        public void SetModal(bool modal) =>
            Native.set_modal(Handle, modal);

        /// <summary>
        /// <para>
        /// Sets the default size of a window. If the window’s “natural” size (its size request) is larger than the
        /// default, the default will be ignored. More generally, if the default size does not obey the geometry hints
        /// for the window (<see cref="SetGeometryHints"/> can be used to set these explicitly), the default size will
        /// be clamped to the nearest permitted size.
        /// </para>
        /// <para>
        /// Unlike <see cref="Widget.SetSizeRequest"/>, which sets a size request for a widget and thus would keep users
        /// from shrinking the window, this function only sets the initial size, just as if the user had resized the
        /// window themselves. Users can still shrink the window again as they normally would. Setting a default size of
        /// -1 means to use the “natural” default size (the size request of the window).
        /// </para>
        /// <para>
        /// For more control over a window’s initial size and how resizing works, investigate <see cref="SetGeometryHints"/>.
        /// </para>
        /// <para>
        /// For some uses, <see cref="Resize"/> is a more appropriate function. <see cref="Resize"/> changes the current
        /// size of the window, rather than the size to be used on initial display. <see cref="Resize"/> always affects
        /// the window itself, not the geometry widget.
        /// </para>
        /// <para>
        /// The default size of a window only affects the first time a window is shown; if a window is hidden
        /// and re-shown, it will remember the size it had prior to hiding, rather than using the default size.
        /// </para>
        /// <para>
        /// Windows can’t actually be 0x0 in size, they must be at least 1x1, but passing 0 for <paramref name="width"/>
        /// and <paramref name="height"/> is OK, resulting in a 1x1 default size.
        /// </para>
        /// <para>
        /// If you use this function to reestablish a previously saved window size, note that the appropriate size to
        /// save is the one returned by <see cref="GetSize"/>. Using the window allocation directly will not work in all
        /// circumstances and can lead to growing or shrinking windows.
        /// </para>
        /// </summary>
        /// <param name="width">Width in pixels, or -1 to unset the default width.</param>
        /// <param name="height">Height in pixels, or -1 to unset the default height.</param>
        public void SetDefaultSize(int width, int height) =>
            Native.set_default_size(Handle, width, height);

        /// <summary>
        /// Like <see cref="SetDefaultSize"/>, but <paramref name="width"/> and <paramref name="height"/> are
        /// interpreted in terms of the base size and increment set with <see cref="SetGeometryHints"/>.
        /// </summary>
        /// <param name="width">Width in resize increments, or -1 to unset the default width.</param>
        /// <param name="height">Height in resize increments, or -1 to unset the default height.</param>
        [Obsolete("This function does nothing. If you want to set a default size, use SetDefaultSize() instead.")]
        public void SetDefaultGeometry(int width, int height) =>
            Native.set_default_geometry(Handle, width, height);

        /// <summary>
        /// This function sets up hints about how a window can be resized by the user. You can set a minimum and maximum
        /// size; allowed resize increments (e.g. for xterm, you can only resize by the size of a character); aspect
        /// ratios; and more. See the <see cref="Gdk.Geometry"/> struct.
        /// </summary>
        /// <param name="geometryWidget">
        /// Widget the geometry hints used to be applied to or <c>null</c>. Since 3.20 this argument is ignored and
        /// GTK behaves as if <c>null</c> was set.
        /// </param>
        /// <param name="geometry">Struct containing geometry information or <c>null</c>.</param>
        /// <param name="geometryMask">Mask indicating which struct fields should be paid attention to.</param>
        public void SetGeometryHints(Widget geometryWidget, Gdk.Geometry geometry, Gdk.WindowHints geometryMask)
        {
            IntPtr geoPtr = Marshal.AllocHGlobal(Marshal.SizeOf(geometry));
            Marshal.StructureToPtr(geometry, geoPtr, true);
            Native.set_geometry_hints(Handle, GetHandle(geometryWidget), geoPtr, geometryMask);
            Marshal.FreeHGlobal(geoPtr);
        }

        /// <summary>
        /// <para>
        /// Window gravity defines the meaning of coordinates passed to <see cref="Move"/>. See <see cref="Move"/> and
        /// GdkGravity for more details.
        /// </para>
        /// <para>
        /// The default window gravity is <see cref="Gdk.Gravity.NorthWest"/> which will typically “do what you mean.”
        /// </para>
        /// </summary>
        /// <param name="gravity">Window gravity.</param>
        public void SetGravity(Gdk.Gravity gravity) =>
            Native.set_gravity(Handle, gravity);

        /// <summary>
        /// Gets the value set by <see cref="SetGravity"/>.
        /// </summary>
        /// <returns>Window gravity.</returns>
        public Gdk.Gravity GetGravity() =>
            Native.get_gravity(Handle);

        /// <summary>
        /// Sets a position constraint for this window. If the old or new constraint is <see cref="Gtk.WindowPosition.CenterAlways"/>,
        /// this will also cause the window to be repositioned to satisfy the new constraint.
        /// </summary>
        /// <param name="position">A position constraint.</param>
        public void SetPosition(WindowPosition position) =>
            Native.set_position(Handle, position);

        /// <summary>
        /// <para>
        /// Dialog windows should be set transient for the main application window they were spawned from.
        /// This allows window managers to e.g. keep the dialog on top of the main window, or center the dialog
        /// over the main window. <see cref="Dialog.WithButtons"/> and other convenience functions in GTK+ will
        /// sometimes call <see cref="SetTransientFor"/> on your behalf.
        /// </para>
        /// <para>
        /// Passing <c>null</c> for parent unsets the current transient window.
        /// </para>
        /// <para>
        /// On Wayland, this function can also be used to attach a new <see cref="WindowType.Popup"/> to a
        /// <see cref="WindowType.Toplevel"/> parent already mapped on screen so that the <see cref="WindowType.Popup"/>
        /// will be created as a subsurface-based window <see cref="Gdk.WindowType.Subsurface"/> which can be positioned
        /// at will relatively to the <see cref="WindowType.Toplevel"/> surface.
        /// </para>
        /// <para>
        /// On Windows, this function puts the child window on top of the parent, much as the window manager would have
        /// done on X.
        /// </para>
        /// </summary>
        /// <param name="parent">Parent <see cref="Window"/> or <c>null</c>.</param>
        public void SetTransientFor(Window? parent) =>
            Native.set_transient_for(Handle, parent is null ? IntPtr.Zero : GetHandle(parent));

        /// <summary>
        /// <para>
        /// Marks window as attached to <paramref name="attachWidget"/>. This creates a logical binding between the
        /// window and the widget it belongs to, which is used by GTK+ to propagate information such as styling or
        /// accessibility to window as if it was a children of <paramref name="attachWidget"/>.
        /// </para>
        /// <para>
        /// Examples of places where specifying this relation is useful are for instance a <see cref="Menu"/> created by
        /// a <see cref="ComboBox"/>, a completion popup window created by <see cref="Entry"/> or a typeahead search
        /// entry created by <see cref="TreeView"/>.
        /// </para>
        /// <para>
        /// Note that this function should not be confused with <see cref="SetTransientFor"/>, which specifies a
        /// window manager relation between two toplevels instead.
        /// </para>
        /// <para>
        /// Passing <c>null</c> for <paramref name="attachWidget"/> detaches the window.
        /// </para>
        /// </summary>
        /// <param name="attachWidget">A <see cref="Widget"/> or <c>null</c>.</param>
        public void SetAttachedTo(Widget? attachWidget) =>
            Native.set_attached_to(Handle, attachWidget is null ? IntPtr.Zero : GetHandle(attachWidget));

        /// <summary>
        /// If setting is <c>true</c>, then destroying the transient parent of window will also destroy window itself.
        /// This is useful for dialogs that shouldn’t persist beyond the lifetime of the main window they're associated
        /// with, for example.
        /// </summary>
        /// <param name="setting">Whether to destroy this <see cref="Window"/> with its transient parent.</param>
        public void SetDestroyWithParent(bool setting) =>
            Native.set_destroy_with_parent(Handle, setting);

        /// <summary>
        /// <para>
        /// If setting is <c>true</c>, then window will request that it’s titlebar should be hidden when maximized.
        /// This is useful for windows that don’t convey any information other than the application name in the titlebar,
        /// to put the available screen space to better use. If the underlying window system does not support the request,
        /// the setting will not have any effect.
        /// </para>
        /// <para>
        /// Note that custom titlebars set with <see cref="SetTitlebar"/> are not affected by this. The application is
        /// in full control of their content and visibility anyway.
        /// </para>
        /// </summary>
        /// <param name="setting">Whether to hide the titlebar when this <see cref="Window"/> is maximized.</param>
        public void SetHideTitlebarWhenMaximized(bool setting) =>
            Native.set_hide_titlebar_when_maximized(Handle, setting);

        /// <summary>
        /// Sets the <see cref="Gdk.Screen"/> where the window is displayed; if the window is already mapped,
        /// it will be unmapped, and then remapped on the new screen.
        /// </summary>
        /// <param name="screen">A <see cref="Gdk.Screen"/>.</param>
        public void SetScreen(Gdk.Screen screen) =>
            Native.set_screen(Handle, GetHandle(screen));

        /// <summary>
        /// Returns the <see cref="Gdk.Screen"/> associated with this instance.
        /// </summary>
        /// <returns>A <see cref="Gdk.Screen"/>.</returns>
        public new Gdk.Screen GetScreen() =>
            WrapPointerAs<Gdk.Screen>(Native.get_screen(Handle));

        /// <summary>
        /// Returns a list of all existing toplevel windows. The widgets in the list are not individually referenced.
        /// If you want to iterate through the list and perform actions involving callbacks that might destroy the widgets,
        /// you must call <c>GLib.List.ForEach(result, Object.Ref)</c> first, and then unref all the widgets afterwards.
        /// </summary>
        /// <returns>List of toplevel widgets.</returns>
        public GLib.List ListToplevels()
        {
            IntPtr listPtr = Native.list_toplevels();
            return Marshal.PtrToStructure<GLib.List>(listPtr);
        }

        /// <summary>
        /// Adds a mnemonic to this window.
        /// </summary>
        /// <param name="keyVal">The mnemonic.</param>
        /// <param name="target">The <see cref="Widget"/> that gets activated by the mnemonic.</param>
        public void AddMnemonic(uint keyVal, Widget target) =>
            Native.add_mnemonic(Handle, keyVal, GetHandle(target));

        /// <summary>
        /// Removes a mnemonic from this window.
        /// </summary>
        /// <param name="keyVal">The mnemonic.</param>
        /// <param name="target">The <see cref="Widget"/> that gets activated by the mnemonic.</param>
        public void RemoveMnemonic(uint keyVal, Widget target) =>
            Native.remove_mnemonic(Handle, keyVal, GetHandle(target));

        /// <summary>
        /// Activates the targets associated with the mnemonic.
        /// </summary>
        /// <param name="keyVal">The mnemonic.</param>
        /// <param name="modifierType">The modifiers.</param>
        /// <returns>
        /// <c>true</c> if the activation is done.
        /// </returns>
        public bool MnemonicActivate(uint keyVal, Gdk.ModifierType modifierType) =>
            Native.mnemonic_activate(Handle, keyVal, modifierType);

        /// <summary>
        /// Activates mnemonics and accelerators for this <see cref="Window"/>. This is normally called by the default
        /// <see cref="Widget.OnKeyPressEvent"/> handler for toplevel windows, however in some cases it may be useful
        /// to call this directly when overriding the standard key handling for a toplevel window.
        /// </summary>
        /// <param name="event">A <see cref="Gdk.EventKey"/>.</param>
        /// <returns>
        /// <c>true</c> if a mnemonic or accelerator was found and activated.
        /// </returns>
        public bool ActivateKey(Gdk.EventKey @event)
        {
            IntPtr eventPtr = Marshal.AllocHGlobal(Marshal.SizeOf(@event));
            Marshal.StructureToPtr(@event, eventPtr, true);
            var result = Native.activate_key(Handle, eventPtr);
            Marshal.FreeHGlobal(eventPtr);

            return result;
        }

        /// <summary>
        /// Propagate a key press or release event to the focus widget and up the focus container chain until a widget
        /// handles <paramref name="event"/>. This is normally called by the default <see cref="Widget.OnKeyPressEvent"/>
        /// and <see cref="Widget.add_OnKeyReleaseEvent"/> handlers for toplevel windows, however in some cases it may
        /// be useful to call this directly when overriding the standard key handling for a toplevel window.
        /// </summary>
        /// <param name="event">A <see cref="Gdk.EventKey"/>.</param>
        /// <returns>
        /// <c>true</c> if a widget in the focus chain handled the event.
        /// </returns>
        public bool PropagateKeyEvent(Gdk.EventKey @event)
        {
            IntPtr eventPtr = Marshal.AllocHGlobal(Marshal.SizeOf(@event));
            Marshal.StructureToPtr(@event, eventPtr, true);
            var result = Native.propagate_key_event(Handle, eventPtr);
            Marshal.FreeHGlobal(eventPtr);

            return result;
        }

        /// <summary>
        /// Retrieves the current focused widget within the window. Note that this is the widget that would have the
        /// focus if the toplevel window focused; if the toplevel window is not focused then <see cref="Widget.HasFocus"/>
        /// will not be <c>true</c> for the widget.
        /// </summary>
        /// <returns>
        /// The currently focused widget, or <c>null</c> if there is none.
        /// </returns>
        public Widget? GetFocus() =>
            GetFocus<Widget>();

        /// <summary>
        /// Retrieves the current focused widget within the window. Note that this is the widget that would have the
        /// focus if the toplevel window focused; if the toplevel window is not focused then <see cref="Widget.HasFocus"/>
        /// will not be <c>true</c> for the widget.
        /// </summary>
        /// <returns>
        /// The currently focused widget, or <c>null</c> if there is none.
        /// </returns>
        public T? GetFocus<T>() where T : Widget
        {
            IntPtr focusPtr = Native.get_focus(Handle);
            return focusPtr == IntPtr.Zero ? null : WrapPointerAs<T>(focusPtr);
        }

        /// <summary>
        /// If <paramref name="focus"/> is not the current focus widget, and is focusable, sets it as the focus widget
        /// for the window. If <paramref name="focus"/> is <c>null</c>, unsets the focus widget for this window.
        /// To set the focus to a particular widget in the toplevel, it is usually more convenient to use
        /// <see cref="Widget.GrabFocus"/>instead of this function.
        /// </summary>
        /// <param name="focus">Widget to be the new focus widget, or <c>null</c> to unset any focus widget for the toplevel window.</param>
        public void SetFocus(Widget? focus) =>
            Native.set_focus(Handle, focus is null ? IntPtr.Zero : GetHandle(focus));

        /// <summary>
        /// The default widget is the widget that’s activated when the user presses Enter in a dialog (for example).
        /// This function sets or unsets the default widget for a <see cref="Window"/>. When setting (rather than unsetting)
        /// the default widget it’s generally easier to call <see cref="Widget.GrabDefault"/> on the widget.
        /// Before making a widget the default widget, you must call <see cref="Widget.SetCanDefault"/> on the widget
        /// you’d like to make the default.
        /// </summary>
        /// <param name="defaultWidget">Widget to be the default, or <c>null</c> to unset the default widget for the toplevel.</param>
        public void SetDefault(Widget? defaultWidget) =>
            Native.set_default(Handle, defaultWidget is null ? IntPtr.Zero : GetHandle(defaultWidget));

        /// <summary>
        /// Presents a window to the user. This function should not be used as when it is called, it is too late to
        /// gather a valid timestamp to allow focus stealing prevention to work correctly.
        /// </summary>
        public void Present() =>
            Native.present(Handle);

        /// <summary>
        /// <para>
        /// Presents a window to the user. This may mean raising the window in the stacking order, deiconifying it,
        /// moving it to the current desktop, and/or giving it the keyboard focus, possibly dependent on the user’s
        /// platform, window manager, and preferences.
        /// </para>
        /// <para>
        /// If window is hidden, this function calls <see cref="Widget.Show"/> as well.
        /// </para>
        /// <para>
        /// This function should be used when the user tries to open a window that’s already open. Say for example
        /// the preferences dialog is currently open, and the user chooses Preferences from the menu a second time;
        /// use <see cref="Present"/> to move the already-open dialog where the user can see it.
        /// </para>
        /// <para>
        /// Presents a window to the user in response to a user interaction. The <paramref name="timestamp"/> should
        /// be gathered when the window was requested to be shown (when clicking a link for example), rather than once
        /// the window is ready to be shown.
        /// </para>
        /// </summary>
        /// <param name="timestamp">
        /// The timestamp of the user interaction (typically a button or key press event)
        /// which triggered this call.
        /// </param>
        public void PresentWithTime(uint timestamp) =>
            Native.present_with_time(Handle, timestamp);

        /// <summary>
        /// <para>
        /// Requests that the window is closed, similar to what happens when a window manager close button is clicked.
        /// </para>
        /// <para>
        /// This function can be used with close buttons in custom titlebars.
        /// </para>
        /// </summary>
        public void Close() =>
            Native.close(Handle);

        /// <summary>
        /// <para>
        /// Asks to iconify (i.e. minimize) this window. Note that you shouldn’t assume the window is definitely
        /// iconified afterward, because other entities (e.g. the user or window manager) could deiconify it again,
        /// or there may not be a window manager in which case iconification isn’t possible, etc. But normally the
        /// window will end up iconified. Just don’t write code that crashes if not.
        /// </para>
        /// <para>
        /// It’s permitted to call this function before showing a window, in which case the window will be iconified
        /// before it ever appears onscreen.
        /// </para>
        /// <para>
        /// You can track iconification via the <see cref="Widget.OnWindowStateEvent"/> signal on <see cref="Widget"/>.
        /// </para>
        /// </summary>
        public void Iconify() =>
            Native.iconify(Handle);

        /// <summary>
        /// <para>
        /// Asks to deiconify (i.e. unminimize) this window. Note that you shouldn’t assume the window is definitely
        /// deiconified afterward, because other entities (e.g. the user or window manager)) could iconify it again
        /// before your code which assumes deiconification gets to run.
        /// </para>
        /// <para>
        /// You can track iconification via the <see cref="Widget.OnWindowStateEvent"/> signal on <see cref="Widget"/>.
        /// </para>
        /// </summary>
        public void Deiconify() =>
            Native.deiconify(Handle);

        /// <summary>
        /// <para>
        /// Asks to stick this window, which means that it will appear on all user desktops. Note that you shouldn’t
        /// assume the window is definitely stuck afterward, because other entities (e.g. the user or window manager
        /// could unstick it again, and some window managers do not support sticking windows. But normally the window
        /// will end up stuck. Just don't write code that crashes if not.
        /// </para>
        /// <para>
        /// It’s permitted to call this function before showing a window.
        /// </para>
        /// <para>
        /// You can track stickiness via the <see cref="Widget.OnWindowStateEvent"/> signal on <see cref="Widget"/>.
        /// </para>
        /// </summary>
        public void Stick() =>
            Native.stick(Handle);

        /// <summary>
        /// <para>
        /// Asks to unstick this window, which means that it will appear on only one of the user’s desktops.
        /// Note that you shouldn’t assume the window is definitely unstuck afterward, because other entities
        /// (e.g. the user or window manager) could stick it again. But normally the window will end up stuck.
        /// Just don’t write code that crashes if not.
        /// </para>
        /// <para>
        /// You can track stickiness via the <see cref="Widget.OnWindowStateEvent"/> signal on <see cref="Widget"/>.
        /// </para>
        /// </summary>
        public void Unstick() =>
            Native.unstick(Handle);

        /// <summary>
        /// <para>
        /// Asks to maximize this window, so that it becomes full-screen. Note that you shouldn’t assume the window is
        /// definitely maximized afterward, because other entities (e.g. the user or window manager) could unmaximize
        /// it again, and not all window managers support maximization. But normally the window will end up maximized.
        /// Just don’t write code that crashes if not.
        /// </para>
        /// <para>
        /// It’s permitted to call this function before showing a window, in which case the window will be maximized
        /// when it appears onscreen initially.
        /// </para>
        /// <para>
        /// You can track maximization via the <see cref="Widget.OnWindowStateEvent"/> signal on <see cref="Widget"/>,
        /// or by listening to notifications on the <see cref="IsMaximized"/> property.
        /// </para>
        /// </summary>
        public void Maximize() =>
            Native.maximize(Handle);

        /// <summary>
        /// <para>
        /// Asks to unmaximize this window. Note that you shouldn’t assume the window is definitely unmaximized
        /// afterward, because other entities (e.g. the user or window manager) could maximize it again, and not all
        /// window managers honor requests to unmaximize. But normally the window will end up unmaximized. Just don’t
        /// write code that crashes if not.
        /// </para>
        /// <para>
        /// You can track maximization via the <see cref="Widget.OnWindowStateEvent"/> signal on <see cref="Widget"/>,
        /// or by listening to notifications on the <see cref="IsMaximized"/> property.
        /// </para>
        /// </summary>
        public void Unmaximize() =>
            Native.unmaximize(Handle);

        /// <summary>
        /// <para>
        /// Asks to place this <see cref="Window"/> in the fullscreen state. Note that you shouldn’t assume the window
        /// is definitely full screen afterward, because other entities (e.g. the user or window manager) could
        /// unfullscreen it again, and not all window managers honor requests to fullscreen windows. But normally
        /// the window will end up fullscreen. Just don’t write code that crashes if not.
        /// </para>
        /// <para>
        /// You can track the fullscreen state via the <see cref="Widget.OnWindowStateEvent"/> signal on <see cref="Widget"/>.
        /// </para>
        /// </summary>
        public void Fullscreen() =>
            Native.fullscreen(Handle);

        /// <summary>
        /// <para>
        /// Asks to place window in the fullscreen state. Note that you shouldn't assume the window is definitely full
        /// screen afterward.
        /// </para>
        /// <para>
        /// You can track the fullscreen state via the <see cref="Widget.OnWindowStateEvent"/> signal on <see cref="Widget"/>.
        /// </para>
        /// </summary>
        /// <param name="screen">A <see cref="Gdk.Screen"/> to draw to.</param>
        /// <param name="monitor">Which monitor to go fullscreen on.</param>
        public void FullscreenOnMonitor(Gdk.Screen screen, int monitor) =>
            Native.fullscreen_on_monitor(Handle, GetHandle(screen), monitor);

        /// <summary>
        /// <para>
        /// Asks to toggle off the fullscreen state for this <see cref="Window"/>. Note that you shouldn’t assume the
        /// window is definitely not full screen afterward, because other entities (e.g. the user or window manager)
        /// could fullscreen it again, and not all window managers honor requests to unfullscreen windows. But normally
        /// the window will end up restored to its normal state. Just don’t write code that crashes if not.
        /// </para>
        /// <para>
        /// You can track the fullscreen state via the <see cref="Widget.OnWindowStateEvent"/> signal on <see cref="Widget"/>.
        /// </para>
        /// </summary>
        public void Unfullscreen() =>
            Native.unfullscreen(Handle);

        /// <summary>
        /// <para>
        /// Asks to keep this <see cref="Window"/> above, so that it stays on top. Note that you shouldn’t assume the
        /// window is definitely above afterward, because other entities (e.g. the user or window manager) could not
        /// keep it above, and not all window managers support keeping windows above. But normally the window will end
        /// kept above. Just don’t write code that crashes if not.
        /// </para>
        /// <para>
        /// It’s permitted to call this function before showing a window, in which case the window will be kept above
        /// when it appears onscreen initially.
        /// </para>
        /// <para>
        /// You can track the above state via the <see cref="Widget.OnWindowStateEvent"/> signal on <see cref="Widget"/>.
        /// </para>
        /// <para>
        /// Note that, according to the Extended Window Manager Hints Specification, the above state is mainly meant for
        /// user preferences and should not be used by applications e.g. for drawing attention to their dialogs.
        /// </para>
        /// </summary>
        /// <param name="setting">Whether to keep this window above other windows.</param>
        public void SetKeepAbove(bool setting) =>
            Native.set_keep_above(Handle, setting);

        /// <summary>
        /// <para>
        /// Asks to keep this <see cref="Window"/> above, so that it stays in bottom. Note that you shouldn’t assume the
        /// window is definitely below afterward, because other entities (e.g. the user or window manager) could not
        /// keep it below, and not all window managers support keeping windows below. But normally the window will end
        /// kept below. Just don’t write code that crashes if not.
        /// </para>
        /// <para>
        /// It’s permitted to call this function before showing a window, in which case the window will be kept below
        /// when it appears onscreen initially.
        /// </para>
        /// <para>
        /// You can track the above state via the <see cref="Widget.OnWindowStateEvent"/> signal on <see cref="Widget"/>.
        /// </para>
        /// <para>
        /// Note that, according to the Extended Window Manager Hints Specification, the above state is mainly meant for
        /// user preferences and should not be used by applications e.g. for drawing attention to their dialogs.
        /// </para>
        /// </summary>
        /// <param name="setting">Whether to keep this window below other windows.</param>
        public void SetKeepBelow(bool setting) =>
            Native.set_keep_below(Handle, setting);

        /// <summary>
        /// Starts resizing a window. This function is used if an application has window resizing controls. When GDK can
        /// support it, the resize will be done using the standard mechanism for the window manager or windowing system.
        /// Otherwise, GDK will try to emulate window resizing, potentially not all that well, depending on the windowing
        /// system.
        /// </summary>
        /// <param name="edge">Position of the resize control.</param>
        /// <param name="button">Mouse button that initialized the drag.</param>
        /// <param name="rootX">X position where the user clicked to initiate the drag, in root window coordinates.</param>
        /// <param name="rootY">Y position where the user clicked to initiate the drag, in root window coordinates.</param>
        /// <param name="timestamp">Timestamp from the click event that initiated the drag.</param>
        public void BeginResizeDrag(Gdk.WindowEdge edge, int button, int rootX, int rootY, uint timestamp) =>
            Native.begin_resize_drag(Handle, edge, button, rootX, rootY, timestamp);

        /// <summary>
        /// Starts moving a window. This function is used if an application has window movement grips. When GDK can
        /// support it, the window movement will be done using the standard mechanism for the window manager or
        /// windowing system. Otherwise, GDK will try to emulate window movement, potentially not all that well,
        /// depending on the windowing system.
        /// </summary>
        /// <param name="button">Mouse button that initialized the drag.</param>
        /// <param name="rootX">X position where the user clicked to initiate the drag, in root window coordinates.</param>
        /// <param name="rootY">Y position where the user clicked to initiate the drag, in root window coordinates.</param>
        /// <param name="timestamp">Timestamp from the click event that initiated the drag.</param>
        public void BeginMoveDrag(int button, int rootX, int rootY, uint timestamp) =>
            Native.begin_move_drag(Handle, button, rootX, rootY, timestamp);

        /// <summary>
        /// <para>
        /// By default, windows are decorated with a title bar, resize controls, etc. Some window managers allow GTK+
        /// to disable these decorations, creating a borderless window. If you set the decorated property to <c>false</c>
        /// using this function, GTK+ will do its best to convince the window manager not to decorate the window.
        /// Depending on the system, this function may not have any effect when called on a window that is already
        /// visible, so you should call it before calling <see cref="Widget.Show"/>.
        /// </para>
        /// <para>
        /// On Windows, this function always works, since there’s no window manager policy involved.
        /// </para>
        /// </summary>
        /// <param name="setting"><c>true</c> to decorate the window.</param>
        public void SetDecorated(bool setting) =>
            Native.set_decorated(Handle, setting);

        /// <summary>
        /// <para>
        /// By default, windows have a close button in the window frame. Some window managers allow GTK+ to disable this
        /// button. If you set the deletable property to <c>false</c> using this function, GTK+ will do its best to
        /// convince the window manager not to show a close button. Depending on the system, this function may not have
        /// any effect when called on a window that is already visible, so you should call it before calling <see cref="Widget.Show"/>.
        /// </para>
        /// <para>
        /// On Windows, this function always works, since there’s no window manager policy involved.
        /// </para>
        /// </summary>
        /// <param name="setting"><c>true</c> to decorate the window as deletable.</param>
        public void SetDeletable(bool setting) =>
            Native.set_deletable(Handle, setting);

        /// <summary>
        /// Sets the mnemonic modifier for this window.
        /// </summary>
        /// <param name="modifier">The modifier mask used to activate mnemonics on this window.</param>
        public void SetMnemonicModifier(Gdk.ModifierType modifier) =>
            Native.set_mnemonic_modifier(Handle, modifier);

        /// <summary>
        /// <para>
        /// By setting the type hint for the window, you allow the window manager to decorate and handle the window in a
        /// way which is suitable to the function of the window in your application.
        /// </para>
        /// <para>
        /// This function should be called before the window becomes visible.
        /// </para>
        /// <para>
        /// <see cref="Dialog.WithButtons"/> and other convenience functions in GTK+ will sometimes call <see cref="SetTypeHint"/>
        /// on your behalf.
        /// </para>
        /// </summary>
        /// <param name="hint">The window type.</param>
        public void SetTypeHint(Gdk.WindowTypeHint hint) =>
            Native.set_type_hint(Handle, hint);

        /// <summary>
        /// Windows may set a hint asking the desktop environment not to display the window in the task bar. This
        /// function sets this hint.
        /// </summary>
        /// <param name="setting"><c>true</c> to keep this window from appearing in the task bar.</param>
        public void SetSkipTaskbarHint(bool setting) =>
            Native.set_skip_taskbar_hint(Handle, setting);

        /// <summary>
        /// Windows may set a hint asking the desktop environment not to display the window in the pager. This
        /// function sets this hint. (A "pager" is any desktop navigation tool such as a workspace switcher that
        /// displays a thumbnail representation of the windows on the screen.).
        /// </summary>
        /// <param name="setting"><c>true</c> to keep this window from appearing in the pager.</param>
        public void SetSkipPagerHint(bool setting) =>
            Native.set_skip_pager_hint(Handle, setting);

        /// <summary>
        /// Windows may set a hint asking the desktop environment to draw the users attention to the window. This
        /// function sets this hint.
        /// </summary>
        /// <param name="setting"><c>true</c> to mark this window as urgent.</param>
        public void SetUrgencyHint(bool setting) =>
            Native.set_urgency_hint(Handle, setting);

        /// <summary>
        /// Windows may set a hint asking the desktop environment not to receive the input focus. This function sets
        /// this hint.
        /// </summary>
        /// <param name="setting"><c>true</c> to let this window receive input focus.</param>
        public void SetAcceptFocus(bool setting) =>
            Native.set_accept_focus(Handle, setting);

        /// <summary>
        /// Windows may set a hint asking the desktop environment not to receive the input focus when the window is
        /// mapped. This function sets this hint.
        /// </summary>
        /// <param name="setting"><c>true</c> to let this window receive input focus on map.</param>
        public void SetFocusOnMap(bool setting) =>
            Native.set_focus_on_map(Handle, setting);

        /// <summary>
        /// <para>
        /// Startup notification identifiers are used by desktop environment to track application startup, to provide
        /// user feedback and other features. This function changes the corresponding property on the underlying
        /// <see cref="Gdk.Window"/>. Normally, startup identifier is managed automatically and you should only use this
        /// function in special cases like transferring focus from other processes. You should use this function before
        /// calling <see cref="Present"/> or any equivalent function generating a window <see cref="Widget.OnMap"/> event.
        /// </para>
        /// <para>
        /// This function is only useful on X11, not with other GTK+ targets.
        /// </para>
        /// </summary>
        /// <param name="id">A string with startup-notification identifier.</param>
        public void SetStartupId(string id) =>
            Native.set_startup_id(Handle, id);

        /// <summary>
        /// <para>
        /// This function is only useful on X11, not with other GTK+ targets.
        /// </para>
        /// <para>
        /// In combination with the window <see cref="Title"/>, the window <see cref="Role"/> allows a window manager to
        /// identify "the same" window when an application is restarted. So for example you might set the <value>"toolbox"</value>
        /// role on your app’s toolbox window, so that when the user restarts their session, the window manager can put
        /// the toolbox back in the same place.
        /// </para>
        /// <para>
        /// If a window already has a unique title, you don’t need to set the role, since the WM can use the title to
        /// identify the window when restoring the session.
        /// </para>
        /// </summary>
        /// <param name="role">Unique identifier for the window to be used when restoring a session.</param>
        public void SetRole(string role) =>
            Native.set_role(Handle, role);

        /// <summary>
        /// Returns whether the window has been set to have decorations such as a title bar via <see cref="SetDecorated"/>.
        /// </summary>
        /// <returns>
        /// <c>true</c> if the window has been set to have decorations.
        /// </returns>
        public bool GetDecorated() =>
            Native.get_decorated(Handle);

        /// <summary>
        /// Returns whether the window has been set to have a close button via <see cref="SetDeletable"/>.
        /// </summary>
        /// <returns>
        /// <c>true</c> if the window has been set to have a close button.
        /// </returns>
        public bool GetDeletable() =>
            Native.get_deletable(Handle);

        /// <summary>
        /// Gets the value set by <see cref="SetDefaultIconList"/>. The list is a copy and should be freed with
        /// <see cref="GLib.List.Free"/>, but the pixbufs in the list have not had their reference count incremented.
        /// </summary>
        /// <returns>Copy of default icon list.</returns>
        public static GLib.List GetDefaultIconList()
        {
            IntPtr listPtr = Native.get_default_icon_list();
            return Marshal.PtrToStructure<GLib.List>(listPtr);
        }

        /// <summary>
        /// Returns the fallback icon name for windows that has been set with <see cref="SetDefaultIconName"/>.
        /// </summary>
        /// <returns>The fallback icon name for windows.</returns>
        public static string? GetDefaultIconName() =>
            Marshal.PtrToStringAnsi(Native.get_default_icon_name());

        /// <summary>
        /// Gets the default size of the window. A value of -1 for the width or height indicates that a default size
        /// has not been explicitly set for that dimension, so the "natural" size of the window will be used.
        /// </summary>
        /// <param name="width">Location to store the default width.</param>
        /// <param name="height">Location to store the default height.</param>
        public void GetDefaultSize(out int width, out int height)
        {
            int w = 0, h = 0;
            Native.get_default_size(Handle, ref w, ref h);
            width = w;
            height = h;
        }

        /// <summary>
        /// Returns whether the window will be destroyed with its transient parent. See <see cref="SetDestroyWithParent"/>.
        /// </summary>
        /// <returns><c>true</c> if the window will be destroyed with its transient parent.</returns>
        public bool GetDestroyWithParent() =>
            Native.get_destroy_with_parent(Handle);

        /// <summary>
        /// Returns whether the window has requested to have its titlebar hidden when maximized. See <see cref="SetHideTitlebarWhenMaximized"/>.
        /// </summary>
        /// <returns><c>true</c> if the window has requested to have its titlebar hidden when maximized.</returns>
        public bool GetHideTitlebarWhenMaximized() =>
            Native.get_hide_titlebar_when_maximized(Handle);

        /// <summary>
        /// Gets the value set by <see cref="SetIcon"/> (or if you've called <see cref="SetIconList"/>, gets the first
        /// icon in the icon list).
        /// </summary>
        /// <returns>Icon for window or <c>null</c> if none.</returns>
        public GdkPixbuf.Pixbuf GetIcon() =>
            WrapPointerAs<GdkPixbuf.Pixbuf>(Native.get_icon(Handle));

        /// <summary>
        /// Retrieves the list of icons set by <see cref="SetIconList"/>. The list is copied, but the reference count on
        /// each member won’t be incremented.
        /// </summary>
        /// <returns>Copy of window's icon list.</returns>
        public GLib.List GetIconList()
        {
            IntPtr listPtr = Native.get_icon_list(Handle);
            return Marshal.PtrToStructure<GLib.List>(listPtr);
        }

        /// <summary>
        /// Returns the name of the themed icon for the window, see <see cref="SetIconName"/>.
        /// </summary>
        /// <returns>The icon name or <c>null</c> if the window has no themed icon.</returns>
        public string? GetIconName() =>
            Marshal.PtrToStringAnsi(Native.get_icon_name(Handle));

        /// <summary>
        /// Returns the mnemonic modifier for this window. See <see cref="SetMnemonicModifier"/>.
        /// </summary>
        /// <returns>The modifier mask used to activate mnemonics on this window.</returns>
        public Gdk.ModifierType GetMnemonicModifier() =>
            Native.get_mnemonic_modifier(Handle);

        /// <summary>
        /// Returns whether the window is modal. See <see cref="SetModal"/>.
        /// </summary>
        /// <returns><c>true</c> if the window is set to be modal and establishes a grab when shown.</returns>
        public bool GetModal() =>
            Native.get_modal(Handle);

        /// <summary>
        /// <para>
        /// This function returns the position you need to pass to <see cref="Move"/> to keep window in its current
        /// position. This means that the meaning of the returned value varies with window gravity. See <see cref="Move"/>
        /// for more details.
        /// </para>
        /// <para>
        /// The reliability of this function depends on the windowing system currently in use. Some windowing systems,
        /// such as Wayland, do not support a global coordinate system, and thus the position of the window will always
        /// be (0, 0). Others, like X11, do not have a reliable way to obtain the geometry of the decorations of a
        /// window if they are provided by the window manager. Additionally, on X11, window manager have been known to
        /// mismanage window gravity, which result in windows moving even if you use the coordinates of the current
        /// position as returned by this function.
        /// </para>
        /// <para>
        /// If you haven’t changed the window gravity, its gravity will be <see cref="Gdk.Gravity.NorthWest"/>. This
        /// means that <see cref="GetPosition"/> gets the position of the top-left corner of the window manager frame
        /// for the window. <see cref="Move"/> sets the position of this same top-left corner.
        /// </para>
        /// <para>
        /// If a window has gravity <see cref="Gdk.Gravity.@static"/> the window manager frame is not relevant, and thus
        /// <see cref="GetPosition"/> will always produce accurate results. However you can’t use static gravity to do
        /// things like place a window in a corner of the screen, because static gravity ignores the window manager
        /// decorations.
        /// </para>
        /// <para>
        /// Ideally, this function should return appropriate values if the window has client side decorations, assuming
        /// that the windowing system supports global coordinates.
        /// </para>
        /// <para>
        /// In practice, saving the window position should not be left to applications, as they lack enough knowledge of
        /// the windowing system and the window manager state to effectively do so. The appropriate way to implement
        /// saving the window position is to use a platform-specific protocol, wherever that is available.
        /// </para>
        /// </summary>
        /// <param name="rootX">Return location for X coordinate of gravity-determined reference point, or <c>null</c>.</param>
        /// <param name="rootY">Return location for Y coordinate of gravity-determined reference point, or <c>null</c>.</param>
        public void GetPosition(out int rootX, out int rootY)
        {
            int x = 0, y = 0;
            Native.get_position(Handle, ref x, ref y);
            rootX = x;
            rootY = y;
        }

        /// <summary>
        /// Returns the role of the window. See <see cref="SetRole"/> for further explanation.
        /// </summary>
        /// <returns>The role of the window if set, or <c>null</c>.</returns>
        public string? GetRole() =>
            Marshal.PtrToStringAnsi(Native.get_role(Handle));

        /// <summary>
        /// <para>
        /// Obtains the current size of this <see cref="Window"/>.
        /// </para>
        /// <para>
        /// If window is not visible on screen, this function return the size GTK+ will suggest to the window manager
        /// for the initial window size (but this is not reliably the same as the size the window manager will actually
        /// select). See: <see cref="SetDefaultSize"/>.
        /// </para>
        /// <para>
        /// Depending on the windowing system and the window manager constraints, the size returned by this function may
        /// not match the size set using <see cref="Resize"/>; additionally, since <see cref="Resize"/> may be implemented
        /// as an asynchronous operation, GTK+ cannot guarantee in any way that this code:
        /// <example>
        /// // width and height are set elsewhere
        /// window.Resize(width, height);
        /// window.GetSize(out int newWidth, out int newHeight);
        /// </example>
        /// will result in <c>newWidth</c> and <c>newHeight</c> matching <c>width</c> and <c>height</c>, respectively.
        /// </para>
        /// <para>
        /// This function will return the logical size of the <see cref="Window"/>, excluding the widgets used in client
        /// side decorations; there is, however, no guarantee that the result will be completely accurate because client
        /// side decoration may include widgets that depend on the user preferences and that may not be visible at the
        /// time you call this function.
        /// </para>
        /// <para>
        /// The dimensions returned by this function are suitable for being stored across sessions; use <see cref="SetDefaultSize"/>
        /// to restore them when before showing the window.
        /// </para>
        /// <para>
        /// To avoid potential race conditions, you should only call this function in response to a size change
        /// notification, for instance inside a handler for the <see cref="Widget.OnSizeAllocate"/> signal, or inside a
        /// handler for the <see cref="Widget.OnConfigureEvent"/> signal:
        /// <example>
        /// public static void OnSizeAllocateCallback(Widget widget, Gdk.Rectangle allocation)
        /// {
        ///   Window window = widget as Window;
        ///   window.GetSize(out int newWidth, out int newHeight);
        ///   ...
        /// }
        /// </example>
        /// Note that, if you connect to the <see cref="Widget.OnSizeAllocate"/> signal, you should not use the dimensions
        /// of the <see cref="Gdk.Rectangle"/> passed to the signal handler, as the allocation may contain client side
        /// decorations added by GTK+, depending on the windowing system in use.
        /// </para>
        /// <para>
        /// If you are getting a window size in order to position the window on the screen, you should, instead, simply
        /// set the window’s semantic type with <see cref="SetTypeHint"/>, which allows the window manager to e.g. center
        /// dialogs. Also, if you set the transient parent of dialogs with <see cref="SetTransientFor"/> window managers
        /// will often center the dialog over its parent window. It's much preferred to let the window manager handle
        /// these cases rather than doing it yourself, because all apps will behave consistently and according to user
        /// or system preferences, if the window manager handles it. Also, the window manager can take into account the
        /// size of the window decorations and border that it may add, and of which GTK+ has no knowledge. Additionally,
        /// positioning windows in global screen coordinates may not be allowed by the windowing system. For more
        /// information, see: <see cref="SetPosition"/>.
        /// </para>
        /// </summary>
        /// <param name="width">Return the width.</param>
        /// <param name="height">Return the height.</param>
        public void GetSize(out int width, out int height)
        {
            int w = 0, h = 0;
            Native.get_size(Handle, ref w, ref h);
            width = w;
            height = h;
        }

        /// <summary>
        /// Retrieves the title of the window. See <see cref="SetTitle"/>.
        /// </summary>
        /// <returns>The title of the window, or <c>null</c> if none has been set explicitly.</returns>
        public string? GetTitle() =>
            Marshal.PtrToStringAnsi(Native.get_title(Handle));

        /// <summary>
        /// Fetches the transient parent for this window. See <see cref="SetTransientFor"/>.
        /// </summary>
        /// <returns>The transient parent for this window, or <c>null</c> if no transient parent has been set.</returns>
        public Window? GetTransientFor()
        {
            IntPtr windowPtr = Native.get_transient_for(Handle);
            return windowPtr == IntPtr.Zero ? null : WrapPointerAs<Window>(windowPtr);
        }

        /// <summary>
        /// Fetches the attach widget for this window. See <see cref="SetAttachedTo"/>.
        /// </summary>
        /// <returns>The widget where the window is attached, or <c>null</c> if the window is not attached to any widget.</returns>
        public Widget? GetAttachedTo()
        {
            IntPtr widgetPtr = Native.get_attached_to(Handle);
            return widgetPtr == IntPtr.Zero ? null : WrapPointerAs<Widget>(widgetPtr);
        }

        /// <summary>
        /// Gets the type hint for this window. See <see cref="SetTypeHint"/>.
        /// </summary>
        /// <returns>The type hint for this <see cref="Window"/>.</returns>
        public Gdk.WindowTypeHint GetTypeHint() =>
            Native.get_type_hint(Handle);

        /// <summary>
        /// Gets the value set by <see cref="SetSkipTaskbarHint"/>.
        /// </summary>
        /// <returns><c>true</c> if window shouldn’t be in taskbar.</returns>
        public bool GetSkipTaskbarHint() =>
            Native.get_skip_taskbar_hint(Handle);

        /// <summary>
        /// Gets the value set by <see cref="SetSkipPagerHint"/>.
        /// </summary>
        /// <returns><c>true</c> if window shouldn’t be in pager.</returns>
        public bool GetSkipPagerHint() =>
            Native.get_skip_pager_hint(Handle);

        /// <summary>
        /// Gets the value set by <see cref="SetUrgencyHint"/>.
        /// </summary>
        /// <returns><c>true</c> if window is urgent.</returns>
        public bool GetUrgencyHint() =>
            Native.get_urgency_hint(Handle);

        /// <summary>
        /// Gets the value set by <see cref="SetAcceptFocus"/>.
        /// </summary>
        /// <returns><c>true</c> if window should receive the input focus.</returns>
        public bool GetAcceptFocus() =>
            Native.get_accept_focus(Handle);

        /// <summary>
        /// Gets the type of the window. See <see cref="WindowType"/>.
        /// </summary>
        /// <returns>The type of the window.</returns>
        public WindowType GetWindowType() =>
            Native.get_window_type(Handle);

        /// <summary>
        /// <para>
        /// Asks the window manager to move this <see cref="Window"/> to the given position. Window managers are free to
        /// ignore this; most window managers ignore requests for initial window positions (instead using a user-defined
        /// placement algorithm) and honor requests after the window has already been shown.
        /// </para>
        /// <para>
        /// Note: the position is the position of the gravity-determined reference point for the window. The gravity
        /// determines two things:
        /// </para>
        /// <list type="number">
        /// <item>
        /// <description>first, the location of the reference point in root window coordinates;</description>
        /// </item>
        /// <item>
        /// <description>and second, which point on the window is positioned at the reference point.</description>
        /// </item>
        /// </list>
        /// <para>
        /// By default the gravity is <see cref="Gdk.Gravity.NorthWest"/>, so the reference point is simply the
        /// <paramref name="x"/>, <paramref name="y"/> supplied to <see cref="Move"/>. The top-left corner of the window
        /// decorations (aka window frame or border) will be placed at <paramref name="x"/>, <paramref name="y"/>.
        /// Therefore, to position a window at the top left of the screen, you want to use the default gravity (which is
        /// <see cref="Gdk.Gravity.NorthWest"/>) and move the window to 0, 0.
        /// </para>
        /// <para>
        /// To position a window at the bottom right corner of the screen, you would set <see cref="Gdk.Gravity.SouthEast"/>,
        /// which means that the reference point is at <paramref name="x"/> + the window width and <paramref name="y"/>
        /// + the window height, and the bottom-right corner of the window border will be placed at that reference point.
        /// So, to place a window in the bottom right corner you would first set gravity to south east, then write:
        /// <c>window.Move(Gdk.Screen.Width() - window_width, Gdk.Screen.Height() - window_height)</c>
        /// (note that this example does not take multi-head scenarios into account).
        /// </para>
        /// <para>
        /// The Extended Window Manager Hints Specification has a nice table of gravities in the "implementation notes"
        /// section.
        /// </para>
        /// <para>
        /// The <see cref="GetPosition"/> documentation may also be relevant.
        /// </para>
        /// </summary>
        /// <param name="x">X coordinate to move window to.</param>
        /// <param name="y">Y coordinate to move window to.</param>
        public void Move(int x, int y) =>
            Native.move(Handle, x, y);

        /// <summary>
        /// <para>
        /// Parses a standard X Window System geometry string - see the manual page for X (type "man X") for details on
        /// this. <see cref="ParseGeometry"/> does work on all GTK+ ports including Win32 but is primarily intended for
        /// an X environment.
        /// </para>
        /// <para>
        /// If either a size or a position can be extracted from the geometry string, <see cref="ParseGeometry"/> returns
        /// <c>true</c> and calls <see cref="SetDefaultSize"/> and/or <see cref="Move"/> to resize/move the window.
        /// </para>
        /// <para>
        /// If <see cref="ParseGeometry"/> returns <c>true</c>, it will also set the <see cref="Gdk.WindowHints.UserPos"/>
        /// and/or <see cref="Gdk.WindowHints.UserSize"/> hints indicating to the window manager that the size/position
        /// of the window was user-specified. This causes most window managers to honor the geometry.
        /// </para>
        /// <para>
        /// Note that for <see cref="ParseGeometry"/> to work as expected, it has to be called when the window has its
        /// "final" size, i.e. after calling <see cref="Widget.ShowAll"/> on the contents and <see cref="SetGeometryHints"/>
        /// on the window.
        /// </para>
        /// </summary>
        /// <param name="geometry">Geometry string.</param>
        /// <returns><c>true</c> if string was parsed successfully.</returns>
        [Obsolete("Geometry handling in GTK is deprecated.")]
        public bool ParseGeometry(string geometry) =>
            Native.parse_geometry(Handle, geometry);

        /// <summary>
        /// Hides this <see cref="Window"/>, then reshows it, resetting the default size and position of the window.
        /// Used by GUI builders only.
        /// </summary>
        [Obsolete("GUI builders can call Hide(), Unrealize() and then Show() on window themselves, if they still need this functionality.")]
        public void ReshowWithInitialSize() =>
            Native.reshow_with_initial_size(Handle);

        /// <summary>
        /// <para>
        /// Resizes the window as if the user had done so, obeying geometry constraints. The default geometry constraint
        /// is that windows may not be smaller than their size request; to override this constraint, call <see cref="Widget.SetSizeRequest"/>
        /// to set the window's request to a smaller value.
        /// </para>
        /// <para>
        /// If <see cref="Resize"/> is called before showing a window for the first time, it overrides any default size
        /// set with <see cref="SetDefaultSize"/>.
        /// </para>
        /// <para>
        /// Windows may not be resized smaller than 1 by 1 pixels.
        /// </para>
        /// <para>
        /// When using client side decorations, GTK+ will do its best to adjust the given size so that the resulting
        /// window size matches the requested size without the title bar, borders and shadows added for the client side
        /// decorations, but there is no guarantee that the result will be totally accurate because these
        /// widgets added for client side decorations depend on the theme and may not be realized or visible at the time
        /// <see cref="Resize"/> is issued.
        /// </para>
        /// <para>
        /// If the <see cref="Window"/> has a titlebar widget (see <see cref="SetTitlebar"/>), then typically, <see cref="Resize"/>
        /// will compensate for the height of the titlebar widget only if the height is known when the resulting
        /// <see cref="Window"/> configuration is issued. For example, if new widgets are added after the <see cref="Window"/>
        /// configuration and cause the titlebar widget to grow in height, this will result in a window content smaller
        /// that specified by <see cref="Resize"/> and not a larger window.
        /// </para>
        /// </summary>
        /// <param name="width">Width in pixels to resize the window to.</param>
        /// <param name="height">Height in pixels to resize the window to.</param>
        public void Resize(int width, int height) =>
            Native.resize(Handle, width, height);

        /// <summary>
        /// Like <see cref="Resize"/>, but <paramref name="width"/> and <paramref name="height"/> are interpreted in
        /// terms of the base size and increment set with <see cref="SetGeometryHints"/>.
        /// </summary>
        /// <param name="width">Width in resize increments to resize the window to.</param>
        /// <param name="height">Height in resize increments to resize the window to.</param>
        [Obsolete("This function does nothing. Use Window.Resize() and compute the geometry yourself.")]
        public void ResizeToGeometry(int width, int height) =>
            Native.resize_to_geometry(Handle, width, height);

        /// <summary>
        /// <para>
        /// Sets an icon list to be used as fallback for windows that haven't had <see cref="SetIconList"/> called on
        /// them to set up a window-specific icon list. This function allows you to set up the icon for all windows in
        /// your app at once.
        /// </para>
        /// <para>
        /// See <see cref="SetIconList"/> for more details.
        /// </para>
        /// </summary>
        /// <param name="list">A list of <see cref="GdkPixbuf.Pixbuf"/>.</param>
        public static void SetDefaultIconList(GLib.List list)
        {
            IntPtr listPtr = Marshal.AllocHGlobal(Marshal.SizeOf(list));
            Marshal.StructureToPtr(list, listPtr, true);
            Native.set_default_icon_list(listPtr);
            Marshal.FreeHGlobal(listPtr);
        }

        /// <summary>
        /// Sets an icon to be used as fallback for windows that haven't had <see cref="SetIcon"/> called on them from a
        /// pixbuf.
        /// </summary>
        /// <param name="icon">The icon.</param>
        public static void SetDefaultIcon(GdkPixbuf.Pixbuf icon) =>
            Native.set_default_icon(GetHandle(icon));

        /// <summary>
        /// Sets an icon to be used as fallback for windows that haven't had <see cref="SetIconList"/> called on them
        /// from a file on disk.
        /// </summary>
        /// <param name="filename">Location of icon file.</param>
        /// <param name="error">The returned error.</param>
        /// <returns><c>true</c> if setting the icon succeeded.</returns>
        public static bool SetDefaultIconFromFile(string filename, out GLib.Error error)
        {
            var result = Native.set_default_icon_from_file(filename, out IntPtr e);
            error = Marshal.PtrToStructure<GLib.Error>(e);
            return result;
        }

        /// <summary>
        /// Sets an icon to be used as fallback for windows that haven't had <see cref="SetIconList"/> called on them
        /// from a named themed icon, see <see cref="SetIconName"/>.
        /// </summary>
        /// <param name="name">The name of the themed icon.</param>
        public static void SetDefaultIconName(string name) =>
            Native.set_default_icon_name(name);

        /// <summary>
        /// <para>
        /// Sets up the icon representing a <see cref="Window"/>. This icon is used when the window is minimized (also
        /// known as iconified). Some window managers or desktop environments may also place it in the window frame, or
        /// display it in other contexts. On others, the icon is not used at all, so your mileage may vary.
        /// </para>
        /// <para>
        /// The icon should be provided in whatever size it was naturally drawn; that is, don’t scale the image before
        /// passing it to GTK+. Scaling is postponed until the last minute, when the desired final size is known, to
        /// allow best quality.
        /// </para>
        /// <para>
        /// If you have your icon hand-drawn in multiple sizes, use <see cref="SetIconList"/>. Then the best size will
        /// be used.
        /// </para>
        /// <para>
        /// This function is equivalent to calling <see cref="SetIconList"/> with a 1-element list.
        /// </para>
        /// <para>
        /// See also <see cref="SetIconList"/> to set the icon for all windows in your application in one go.
        /// </para>
        /// </summary>
        /// <param name="icon">Icon image, or <c>null</c>.</param>
        public void SetIcon(GdkPixbuf.Pixbuf? icon) =>
            Native.set_icon(Handle, icon is null ? IntPtr.Zero : GetHandle(icon));

        /// <summary>
        /// <para>
        /// Sets up the icon representing a <see cref="Window"/>. The icon is used when the window is minimized (also
        /// known as iconified). Some window managers or desktop environments may also place it in the window frame, or
        /// display it in other contexts. On others, the icon is not used at all, so your mileage may vary.
        /// </para>
        /// <para>
        /// <see cref="SetIconList"/> allows you to pass in the same icon in several hand-drawn sizes. The list should
        /// contain the natural sizes your icon is available in; that is, don’t scale the image before passing it to
        /// GTK+. Scaling is postponed until the last minute, when the desired final size is known, to allow best quality.
        /// </para>
        /// <para>
        /// By passing several sizes, you may improve the final image quality of the icon, by reducing or eliminating
        /// automatic image scaling.
        /// </para>
        /// <para>
        /// Recommended sizes to provide: 16x16, 32x32, 48x48 at minimum, and larger images (64x64, 128x128) if you have
        /// them.
        /// </para>
        /// <para>
        /// See also <see cref="SetDefaultIconList"/> to set the icon for all windows in your application in one go.
        /// </para>
        /// <para>
        /// Note that transient windows (those who have been set transient for another window using <see cref="SetTransientFor"/>)
        /// will inherit their icon from their transient parent. So there’s no need to explicitly set the icon on
        /// transient windows.
        /// </para>
        /// </summary>
        /// <param name="list">A list of <see cref="GdkPixbuf.Pixbuf"/>.</param>
        public void SetIconList(GLib.List list)
        {
            IntPtr listPtr = Marshal.AllocHGlobal(Marshal.SizeOf(list));
            Marshal.StructureToPtr(list, listPtr, true);
            Native.set_icon_list(Handle, listPtr);
            Marshal.FreeHGlobal(listPtr);
        }

        /// <summary>
        /// <para>
        /// Sets the icon for this <see cref="Window"/>.
        /// </para>
        /// <para>
        /// This function is equivalent to calling <see cref="SetIcon"/> with a pixbuf created by loading the image from
        /// filename.
        /// </para>
        /// </summary>
        /// <param name="filename">Location of icon file.</param>
        /// <param name="error">The returned error.</param>
        /// <returns><c>true</c> if setting the icon succeeded.</returns>
        public bool SetIconFromFile(string filename, out GLib.Error error)
        {
            var result = Native.set_icon_from_file(Handle, filename, out IntPtr e);
            error = Marshal.PtrToStructure<GLib.Error>(e);
            return result;
        }

        /// <summary>
        /// <para>
        /// Sets the icon for the window from a named themed icon. See the docs for <see cref="IconTheme"/> for more
        /// details. On some platforms, the window icon is not used at all.
        /// </para>
        /// <para>
        /// Note that this has nothing to do with the WM_ICON_NAME property which is mentioned in the ICCCM.
        /// </para>
        /// </summary>
        /// <param name="name">The name of the themed icon.</param>
        public void SetIconName(string name) =>
            Native.set_icon_name(Handle, name);

        /// <summary>
        /// <para>
        /// By default, after showing the first <see cref="Window"/>, GTK+ calls <see cref="Gdk.Global.NotifyStartupComplete"/>.
        /// Call this function to disable the automatic startup notification. You might do this if your first window is
        /// a splash screen, and you want to delay notification until after your real main window has been shown,
        /// for example.
        /// </para>
        /// <para>
        /// In that example, you would disable startup notification temporarily, show your splash screen, then re-enable
        /// it so that showing the main window would automatically result in notification.
        /// </para>
        /// </summary>
        /// <param name="setting"><c>true</c> to automatically do startup notification.</param>
        public static void SetAutoStartupNotification(bool setting) =>
            Native.set_auto_startup_notification(setting);

        /// <summary>
        /// Fetches the requested opacity for this window. See <see cref="SetOpacity"/>.
        /// </summary>
        /// <returns>
        /// The requested opacity of the window.
        /// </returns>
        [Obsolete("Use Widget.GetOpacity() instead.")]
        public double GetOpacity() =>
            Native.get_opacity(Handle);

        /// <summary>
        /// <para>
        /// Request the windowing system to make window partially transparent, with <paramref name="opacity"/>
        /// <value>0</value> being fully transparent and <value>1</value> fully opaque. (Values of the opacity parameter
        /// are clamped to the [0,1] range.) On X11 this has any effect only on X screens with a compositing manager
        /// running. See <see cref="Widget.IsComposited"/>. On Windows it should work always.
        /// </para>
        /// <para>
        /// Note that setting a window’s opacity after the window has been shown causes it to flicker once on Windows.
        /// </para>
        /// </summary>
        /// <param name="opacity">Desired opacity, between 0 and 1.</param>
        public void SetOpacity(double opacity) =>
            Native.set_opacity(Handle, opacity);

        /// <summary>
        /// Gets the value of the <see cref="MnemonicsVisible"/> property.
        /// </summary>
        /// <returns><c>true</c> if mnemonics are supposed to be visible in this window.</returns>
        public bool GetMnemonicsVisible() =>
            Native.get_mnemonics_visible(Handle);

        /// <summary>
        /// Sets the value of the <see cref="MnemonicsVisible"/> property.
        /// </summary>
        /// <param name="setting">The new value</param>
        public void SetMnemonicsVisible(bool setting) =>
            Native.set_mnemonics_visible(Handle, setting);

        /// <summary>
        /// Gets the value of the <see cref="FocusVisible"/> property.
        /// </summary>
        /// <returns><c>true</c> if "focus rectangles" are supposed to be visible in this window.</returns>
        public bool GetFocusVisible() =>
            Native.get_focus_visible(Handle);

        /// <summary>
        /// Sets the value of the <see cref="FocusVisible"/> property.
        /// </summary>
        /// <param name="setting">The new value</param>
        public void SetFocusVisible(bool setting) =>
            Native.set_focus_visible(Handle, setting);

        /// <summary>
        /// Gets the value of the <see cref="HasResizeGrip"/> property.
        /// </summary>
        /// <returns><c>true</c> if the window has a resize grip.</returns>
        [Obsolete("Resize grips have been removed.")]
        public bool GetHasResizeGrip() =>
            Native.get_has_resize_grip(Handle);

        /// <summary>
        /// Sets the value of the <see cref="HasResizeGrip"/> property.
        /// </summary>
        /// <param name="setting"><c>true</c> to allow resize grip.</param>
        [Obsolete("Resize grips have been removed.")]
        public void SetHasResizeGrip(bool setting) =>
            Native.set_has_resize_grip(Handle, setting);

        /// <summary>
        /// Determines whether a resize grip is visible for the specified window.
        /// </summary>
        /// <returns><c>true</c> if resize grip exist and is visible.</returns>
        [Obsolete("Resize grips have been removed.")]
        public bool ResizeGripIsVisible() =>
            Native.resize_grip_is_visible(Handle);

        /// <summary>
        /// If a window has a resize grip, this will retrieve the grip position, width and height into the specified
        /// <see cref="Gdk.Rectangle"/>.
        /// </summary>
        /// <param name="rect">A GdkRectangle which we should store the resize grip area.</param>
        /// <returns><c>true</c> if the resize grip’s area was retrieved.</returns>
        [Obsolete("Resize grips have been removed.")]
        public bool GetResizeGripArea(out Gdk.Rectangle rect)
        {
            rect = new Gdk.Rectangle();
            IntPtr rectPtr = Marshal.AllocHGlobal(Marshal.SizeOf(rect));
            var result = Native.get_resize_grip_area(Handle, rectPtr);
            rect = Marshal.PtrToStructure<Gdk.Rectangle>(rectPtr);
            Marshal.FreeHGlobal(rectPtr);
            return result;
        }

        /// <summary>
        /// Gets the <see cref="Gtk.Application"/> associated with the window (if any).
        /// </summary>
        /// <returns>A <see cref="Gtk.Application"/>, or <c>null</c>.</returns>
        public Application? GetApplication()
        {
            IntPtr appPtr = Native.get_application(Handle);
            return appPtr == IntPtr.Zero ? null : WrapPointerAs<Application>(appPtr);
        }

        /// <summary>
        /// <para>
        /// Sets or unsets the <see cref="Gtk.Application"/> associated with the window.
        /// </para>
        /// <para>
        /// The application will be kept alive for at least as long as it has any windows associated with it (see <see cref="Gio.Application.Hold"/>
        /// for a way to keep it alive without windows).
        /// </para>
        /// <para>
        /// Normally, the connection between the application and the window will remain until the window is destroyed,
        /// but you can explicitly remove it by setting the application to <c>null</c>.
        /// </para>
        /// <para>
        /// This is equivalent to calling <see cref="Gtk.Application.RemoveWindow"/> and/or <see cref="Gtk.Application.AddWindow"/>
        /// on the old/new applications as relevant.
        /// </para>
        /// </summary>
        /// <param name="application">A <see cref="Gtk.Application"/> or <c>null</c> to unset.</param>
        public void SetApplication(Application? application) =>
            Native.set_application(Handle, application is null ? IntPtr.Zero : GetHandle(application));

        /// <summary>
        /// <para>
        /// Tells GTK+ whether to drop its extra reference to the window when <see cref="Widget.Destroy"/> is called.
        /// </para>
        /// <para>
        /// This function is only exported for the benefit of language bindings which may need to keep the window alive
        /// until their wrapper object is garbage collected. There is no justification for ever calling this function in
        /// an application.
        /// </para>
        /// </summary>
        /// <param name="setting">The new value.</param>
        public void SetHasUserRefCount(bool setting) =>
            Native.set_has_user_ref_count(Handle, setting);

        /// <summary>
        /// <para>
        /// Sets a custom titlebar for this <see cref="Window"/>.
        /// </para>
        /// <para>
        /// A typical widget used here is <see cref="HeaderBar"/>, as it provides various features expected of a titlebar
        /// while allowing the addition of child widgets to it.
        /// </para>
        /// <para>
        /// If you set a custom titlebar, GTK+ will do its best to convince the window manager not to put its own titlebar
        /// on the window. Depending on the system, this function may not work for a window that is already visible, so
        /// you set the titlebar before calling <see cref="Widget.Show"/>.
        /// </para>
        /// </summary>
        /// <param name="titlebar">The <see cref="Widget"/> to use as titlebar.</param>
        public void SetTitlebar(Widget? titlebar) =>
            Native.set_titlebar(Handle, titlebar is null ? IntPtr.Zero : GetHandle(titlebar));

        /// <summary>
        /// Returns the custom titlebar that has been set with <see cref="SetTitlebar"/>.
        /// </summary>
        /// <returns>The custom titlebar, or <c>null</c>.</returns>
        public Widget? GetTitlebar()
        {
            IntPtr ptr = Native.get_titlebar(Handle);
            return ptr == IntPtr.Zero ? null : WrapPointerAs<Widget>(ptr);
        }

        /// <summary>
        /// Opens or closes the interactive debugger, which offers access to the widget hierarchy of the application and
        /// to useful debugging tools.
        /// </summary>
        /// <param name="enable"><c>true</c> to enable interactive debugging.</param>
        public static void SetInteractiveDebugging(bool enable) =>
            Native.set_interactive_debugging(enable);

        #endregion
    }
}

#pragma warning restore 618
