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
            => WrapPointerAs<Widget>(Native.get_content_area(Handle));

        // TODO: Allow for arbitrary response IDs, not just
        // the ones in Gtk.ResponseType
        public Widget AddButton(string buttonText, ResponseType responseId)
            => WrapPointerAs<Widget>(Native.add_button(Handle, buttonText, responseId));

        public int Run() => Native.run(Handle);
    }
}
