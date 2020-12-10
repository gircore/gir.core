using System;

namespace Gtk
{
    public partial class RecentChooserMenu
    {
        #region IRecentChooser Implementation

        public RecentFilter Filter
        {
            get => GetProperty(RecentChooser.FilterProperty);
            set => SetProperty(RecentChooser.FilterProperty, value);
        }

        public int Limit
        {
            get => GetProperty(RecentChooser.LimitProperty);
            set => SetProperty(RecentChooser.LimitProperty, value);
        }

        public bool LocalOnly
        {
            get => GetProperty(RecentChooser.LocalOnlyProperty);
            set => SetProperty(RecentChooser.LocalOnlyProperty, value);
        }

        public RecentManager RecentManager
        {
            get => GetProperty(RecentChooser.RecentManagerProperty);
            set => SetProperty(RecentChooser.RecentManagerProperty, value);
        }

        public bool SelectMultiple
        {
            get => GetProperty(RecentChooser.SelectMultipleProperty);
            set => SetProperty(RecentChooser.SelectMultipleProperty, value);
        }

        public bool ShowIcons
        {
            get => GetProperty(RecentChooser.ShowIconsProperty);
            set => SetProperty(RecentChooser.ShowIconsProperty, value);
        }

        public bool ShowNotFound
        {
            get => GetProperty(RecentChooser.ShowNotFoundProperty);
            set => SetProperty(RecentChooser.ShowNotFoundProperty, value);
        }

        public bool ShowPrivate
        {
            get => GetProperty(RecentChooser.ShowPrivateProperty);
            set => SetProperty(RecentChooser.ShowPrivateProperty, value);
        }

        public bool ShowTips
        {
            get => GetProperty(RecentChooser.ShowTipsProperty);
            set => SetProperty(RecentChooser.ShowTipsProperty, value);
        }

        public RecentSortType SortType
        {
            get => GetProperty(RecentChooser.SortTypeProperty);
            set => SetProperty(RecentChooser.SortTypeProperty, value);
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
