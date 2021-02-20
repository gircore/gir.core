namespace Gtk
{
    public partial class AppChooserWidget
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
