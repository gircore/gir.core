using System;
using GObject;

namespace Gtk
{
    public partial class TextView
    {
        #region Properties

        public static readonly Property<TextBuffer> BufferProperty = Property<TextBuffer>.Register<TextView>(
            Native.BufferProperty,
            nameof(Buffer),
            o => o.Buffer,
            (o, v) => o.Buffer = v
        );

        public static readonly Property<bool> EditableProperty = Property<bool>.Register<TextView>(
            Native.EditableProperty,
            nameof(Editable),
            o => o.Editable,
            (o, v) => o.Editable = v
        );

        public TextBuffer Buffer
        {
            get => GetProperty(BufferProperty);
            set => SetProperty(BufferProperty, value);
        }

        public bool Editable
        {
            get => GetProperty(EditableProperty);
            set => SetProperty(EditableProperty, value);
        }

        #endregion

        #region Constructors

        public TextView()
            : base(Array.Empty<ConstructParameter>())
        { }

        #endregion

        #region IScrollable Implementation

        public Adjustment Hadjustment
        {
            get => GetProperty(Scrollable.HadjustmentProperty);
            set => SetProperty(Scrollable.HadjustmentProperty, value);
        }

        public ScrollablePolicy HscrollPolicy
        {
            get => GetProperty(Scrollable.HscrollPolicyProperty);
            set => SetProperty(Scrollable.HscrollPolicyProperty, value);
        }

        public Adjustment Vadjustment
        {
            get => GetProperty(Scrollable.VadjustmentProperty);
            set => SetProperty(Scrollable.VadjustmentProperty, value);
        }

        public ScrollablePolicy VscrollPolicy
        {
            get => GetProperty(Scrollable.VscrollPolicyProperty);
            set => SetProperty(Scrollable.VscrollPolicyProperty, value);
        }

        #endregion
    }
}
