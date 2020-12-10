using System;
using GLib;

#pragma warning disable 618

namespace Gtk
{
    public partial class Switch
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

        #region IActivatable Implementation

        [Obsolete]
        public Action RelatedAction
        {
            get => GetProperty(Activatable.RelatedActionProperty);
            set => SetProperty(Activatable.RelatedActionProperty, value);
        }

        [Obsolete]
        public bool UseActionAppearance
        {
            get => GetProperty(Activatable.UseActionAppearanceProperty);
            set => SetProperty(Activatable.UseActionAppearanceProperty, value);
        }

        #endregion
    }
}
