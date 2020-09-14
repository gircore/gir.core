using System;
using System.Reflection;
using GObject;

namespace Gtk
{
    public partial class Container
    {
        #region Properties

        public static readonly Property<uint> BorderWidthProperty = GObject.Property<uint>.Register<Container>(
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

        public static readonly Property<Widget> ChildProperty = GObject.Property<Widget>.Register<Container>(
            "child",
            nameof(Child),
            set: (o, v) => o.Child = v
        );

        public Widget Child
        {
            set => SetProperty(ChildProperty, value);
        }

        public static readonly Property<ResizeMode> ResizeModeProperty = GObject.Property<ResizeMode>.Register<Container>(
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

        public static readonly Signal<SignalArgs> AddSignal = Signal<SignalArgs>.Wrap("add");

        // Renamed to Added because we have a Add() method down
        public event EventHandler<SignalArgs> Added
        {
            add => AddSignal.Connect(this, value, true);
            remove => AddSignal.Disconnect(this, value);
        }

        public static readonly Signal<SignalArgs> CheckResizeSignal = Signal<SignalArgs>.Wrap("check-resize");

        public event EventHandler<SignalArgs> CheckResize
        {
            add => CheckResizeSignal.Connect(this, value, true);
            remove => CheckResizeSignal.Disconnect(this, value);
        }

        public static readonly Signal<SignalArgs> RemoveSignal = Signal<SignalArgs>.Wrap("remove");

        // Renamed to Removed because we have a Remove() method down
        public event EventHandler<SignalArgs> Removed
        {
            add => RemoveSignal.Connect(this, value, true);
            remove => RemoveSignal.Disconnect(this, value);
        }

        public static readonly Signal<SignalArgs> SetFocusChildSignal = Signal<SignalArgs>.Wrap("remove");

        public event EventHandler<SignalArgs> SetFocusChild
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
