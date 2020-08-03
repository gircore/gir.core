using System;

namespace Gtk
{
    public class Dialog : Window
    {
        #region Properties
        private Container? contentArea;
        public Container ContentArea => contentArea ??= new Container(Sys.Dialog.get_content_area(this));
        #endregion Properties

        public Dialog() : this(Sys.Dialog.@new()) { }
        internal Dialog(IntPtr handle) : base(handle){ }
        
        public int Run() => Sys.Dialog.run(this);
    }
}