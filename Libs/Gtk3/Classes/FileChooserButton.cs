using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GObject;

namespace Gtk
{
    public partial class FileChooserButton
    {
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
            ConstructParameter.With(DialogProperty, dialog)) {}

        public static FileChooserButton New(string title, FileChooserAction action)
            => new FileChooserButton(Native.@new(title, action));
    }
}
