using Gtk.Core;

namespace GtkApp
{
    public class MyBox : GBox
    {
        [Connect]
        private GLabel Label1 = default!;

        [Connect]
        private GLabel Label2 = default!;

        [Connect]
        private GLabel Label3 = default!;

        public MyBox() : base("box.glade") 
        {
        }
    }
}