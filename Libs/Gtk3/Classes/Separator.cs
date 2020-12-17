namespace Gtk
{
    public partial class Separator
    {
        #region IOrientable Implementation

        public Orientation Orientation
        {
            get => GetProperty(Orientable.OrientationProperty);
            set => SetProperty(Orientable.OrientationProperty, value);
        }

        #endregion
    }
}
