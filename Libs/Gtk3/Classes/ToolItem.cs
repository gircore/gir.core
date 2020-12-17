using System;

namespace Gtk
{
    public partial class ToolItem
    {
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
