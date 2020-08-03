using Gtk;

namespace GtkApp
{
    public class MyBox : Box
    {
        [Connect]
        private Label Label1 = default!;

        [Connect]
        private Label Label2 = default!;

        [Connect]
        private Label Label3 = default!;

        public MyBox() : base("box.glade") 
        {
        }
    }
}