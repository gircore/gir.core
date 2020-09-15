using System;
using System.Reflection;
using GObject;

namespace Gtk
{
    public partial class Container
    {
        #region Properties

        public static readonly Property<uint> BorderWidthProperty = Property<uint>.Register<Container>(
            "border-width",
            nameof(BorderWidth),
            get: (o) => o.BorderWidth,
            set: (o, v) => o.BorderWidth = v
        );

        public uint BorderWidth
        {
            get => GetProperty(BorderWidthProperty);
            set => SetProperty(BorderWidthProperty, value);
        }

        public static readonly Property<Widget> ChildProperty = Property<Widget>.Register<Container>(
            "child",
            nameof(Child),
            set: (o, v) => o.Child = v
        );

        public Widget Child
        {
            set => SetProperty(ChildProperty, value);
        }

        public static readonly Property<ResizeMode> ResizeModeProperty = Property<ResizeMode>.Register<Container>(
            "resize-mode",
            nameof(ResizeMode),
            get: (o) => o.ResizeMode,
            set: (o, v) => o.ResizeMode = v
        );

        public ResizeMode ResizeMode
        {
            get => GetProperty(ResizeModeProperty);
            set => SetProperty(ResizeModeProperty, value);
        }

        #endregion

        #region Signals

        public static readonly Signal AddSignal = Signal.Wrap("add");

        public event EventHandler<SignalArgs> OnAdd
        {
            add => AddSignal.Connect(this, value, true);
            remove => AddSignal.Disconnect(this, value);
        }

        public static readonly Signal CheckResizeSignal = Signal.Wrap("check-resize");

        public event EventHandler<SignalArgs> OnCheckResize
        {
            add => CheckResizeSignal.Connect(this, value, true);
            remove => CheckResizeSignal.Disconnect(this, value);
        }

        public static readonly Signal RemoveSignal = Signal.Wrap("remove");

        public event EventHandler<SignalArgs> OnRemove
        {
            add => RemoveSignal.Connect(this, value, true);
            remove => RemoveSignal.Disconnect(this, value);
        }

        public static readonly Signal SetFocusChildSignal = Signal.Wrap("remove");

        public event EventHandler<SignalArgs> OnSetFocusChild
        {
            add => SetFocusChildSignal.Connect(this, value, true);
            remove => SetFocusChildSignal.Disconnect(this, value);
        }

        #endregion

        #region Methods

        public void Add(Widget widget) => Sys.Container.add(Handle, GetHandle(widget));
        public void Remove(Widget widget) => Sys.Container.remove(Handle, GetHandle(widget));

        #endregion
    }
}
