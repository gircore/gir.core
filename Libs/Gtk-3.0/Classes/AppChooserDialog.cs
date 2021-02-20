namespace Gtk
{
    public partial class AppChooserDialog
    {
        #region IAppChooser Implementation

        public string ContentType
        {
            get => GetProperty(AppChooser.ContentTypeProperty);
            set => SetProperty(AppChooser.ContentTypeProperty, value);
        }

        #endregion
    }
}
