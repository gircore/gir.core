using GObject;

namespace Gtk
{
    public partial class Dialog
    {
        /// <summary>
        /// Creates a new dialog.
        /// </summary>
        public Dialog() { }
        
        public Widget GetContentArea()
            => WrapHandle<Box>(Native.get_content_area(Handle), false);

        // TODO: Allow for arbitrary response IDs, not just
        // the ones in Gtk.ResponseType
        public Widget AddButton(string buttonText, ResponseType responseId)
            => WrapHandle<Widget>(Native.add_button(Handle, buttonText, responseId), false);

        public int Run() => Native.run(Handle);
    }
}
