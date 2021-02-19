namespace Gtk
{
    public partial class Entry
    {
        #region ICellEditable Implementation

        public bool EditingCanceled
        {
            get => GetProperty(CellEditable.EditingCanceledProperty);
            set => SetProperty(CellEditable.EditingCanceledProperty, value);
        }

        #endregion
    }
}
