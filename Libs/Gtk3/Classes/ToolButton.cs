using GLib;

namespace Gtk
{
    public partial class ToolButton
    {
        #region IActionable Implementation

        public string ActionName
        {
            get => GetProperty(Actionable.ActionNameProperty);
            set => SetProperty(Actionable.ActionNameProperty, value);
        }

        public Variant ActionTarget
        {
            get => GetProperty(Actionable.ActionTargetProperty);
            set => SetProperty(Actionable.ActionTargetProperty, value);
        }

        #endregion
    }
}
