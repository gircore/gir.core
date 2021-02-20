using Pango;

namespace Gtk
{
    public partial class FontChooserDialog
    {
        #region IFontChooser Implementation

        public string Font
        {
            get => GetProperty(FontChooser.FontProperty);
            set => SetProperty(FontChooser.FontProperty, value);
        }

        public FontDescription FontDesc
        {
            get => GetProperty(FontChooser.FontDescProperty);
            set => SetProperty(FontChooser.FontDescProperty, value);
        }

        public string FontFeatures
        {
            get => GetProperty(FontChooser.FontFeaturesProperty);
            set => SetProperty(FontChooser.FontFeaturesProperty, value);
        }

        public string Language
        {
            get => GetProperty(FontChooser.LanguageProperty);
            set => SetProperty(FontChooser.LanguageProperty, value);
        }

        public FontChooserLevel Level
        {
            get => GetProperty(FontChooser.LevelProperty);
            set => SetProperty(FontChooser.LevelProperty, value);
        }

        public string PreviewText
        {
            get => GetProperty(FontChooser.PreviewTextProperty);
            set => SetProperty(FontChooser.PreviewTextProperty, value);
        }

        public bool ShowPreviewEntry
        {
            get => GetProperty(FontChooser.ShowPreviewEntryProperty);
            set => SetProperty(FontChooser.ShowPreviewEntryProperty, value);
        }

        #endregion
    }
}
