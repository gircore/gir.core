namespace Gtk
{
    public partial class IconView
    {
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
