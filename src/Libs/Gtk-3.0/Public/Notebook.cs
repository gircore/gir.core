namespace Gtk
{
    public partial class Notebook
    {
        public Notebook() { }

        public Widget this[string label]
        {
            set => AppendPage(value, Label.New(label));
        }
    }
}
