namespace Gtk
{
    public partial class Dialog
    {
        #region Constructors
        
        /// <summary>
        /// Creates a new dialog.
        /// </summary>
        public Dialog() : this(Native.@new()) { }
        
        #endregion
        
        #region Methods

        public int Run() => Native.run(Handle);

        #endregion
    }
}
