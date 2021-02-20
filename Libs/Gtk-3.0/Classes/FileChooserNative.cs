namespace Gtk
{
    public partial class FileChooserNative
    {
        #region IFileChooser Implementation

        public FileChooserAction Action
        {
            get => GetProperty(FileChooser.ActionProperty);
            set => SetProperty(FileChooser.ActionProperty, value);
        }

        public bool CreateFolders
        {
            get => GetProperty(FileChooser.CreateFoldersProperty);
            set => SetProperty(FileChooser.CreateFoldersProperty, value);
        }

        public bool DoOverwriteConfirmation
        {
            get => GetProperty(FileChooser.DoOverwriteConfirmationProperty);
            set => SetProperty(FileChooser.DoOverwriteConfirmationProperty, value);
        }

        public Widget ExtraWidget
        {
            get => GetProperty(FileChooser.ExtraWidgetProperty);
            set => SetProperty(FileChooser.ExtraWidgetProperty, value);
        }

        public FileFilter Filter
        {
            get => GetProperty(FileChooser.FilterProperty);
            set => SetProperty(FileChooser.FilterProperty, value);
        }

        public bool LocalOnly
        {
            get => GetProperty(FileChooser.LocalOnlyProperty);
            set => SetProperty(FileChooser.LocalOnlyProperty, value);
        }

        public Widget PreviewWidget
        {
            get => GetProperty(FileChooser.PreviewWidgetProperty);
            set => SetProperty(FileChooser.PreviewWidgetProperty, value);
        }

        public bool PreviewWidgetActive
        {
            get => GetProperty(FileChooser.PreviewWidgetActiveProperty);
            set => SetProperty(FileChooser.PreviewWidgetActiveProperty, value);
        }

        public bool SelectMultiple
        {
            get => GetProperty(FileChooser.SelectMultipleProperty);
            set => SetProperty(FileChooser.SelectMultipleProperty, value);
        }

        public bool ShowHidden
        {
            get => GetProperty(FileChooser.ShowHiddenProperty);
            set => SetProperty(FileChooser.ShowHiddenProperty, value);
        }

        public bool UsePreviewLabel
        {
            get => GetProperty(FileChooser.UsePreviewLabelProperty);
            set => SetProperty(FileChooser.UsePreviewLabelProperty, value);
        }

        #endregion
    }
}
