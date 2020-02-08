using System;

namespace Gtk.Core
{
    public partial class GDialog : GWindow
    {
        #region Properties
        private GContainer? contentArea;
        public GContainer ContentArea => contentArea ??= new GContainer(Gtk.Dialog.get_content_area(this));
        #endregion Properties

        public GDialog() : this(Gtk.Dialog.@new()) { }
        internal GDialog(IntPtr handle) : base(handle){ }
        
        public int Run() => Gtk.Dialog.run(this);
    }
}