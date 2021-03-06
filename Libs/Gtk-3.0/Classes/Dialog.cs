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
            => WrapHandle<Box>(Native.Methods.GetContentArea(Handle), false);

        // TODO: Allow for arbitrary response IDs, not just
        // the ones in Gtk.ResponseType
        public Widget AddButton(string buttonText, ResponseType responseId)
            => WrapHandle<Widget>(Native.Methods.AddButton(Handle, buttonText, responseId), false);

        public int Run() => Native.Methods.Run(Handle);
    }
}
