using GObject;

namespace Gtk
{
    public partial class FileChooserButton
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

        public static readonly Property<string> TitleProperty = Property<string>.Register<FileChooserButton>(
            Native.TitleProperty,
            nameof(Title),
            (o) => o.Title,
            (o, v) => o.Title = v
        );

        public string Title
        {
            get => GetProperty(TitleProperty);
            set => SetProperty(TitleProperty, value);
        }

        public static readonly Property<FileChooserDialog> DialogProperty = Property<FileChooserDialog>.Register<FileChooserButton>(
            Native.DialogProperty,
            nameof(Dialog),
            (o) => o.Dialog,
            (o, v) => o.Dialog = v
        );

        public FileChooserDialog Dialog
        {
            get => GetProperty(DialogProperty);
            set => SetProperty(DialogProperty, value);
        }

        public FileChooserButton(string title, FileChooserDialog dialog) : this(
            ConstructParameter.With(TitleProperty, title),
            ConstructParameter.With(DialogProperty, dialog))
        { }

        public static FileChooserButton New(string title, FileChooserAction action)
            => new FileChooserButton(Native.@new(title, action), false);
    }
}
