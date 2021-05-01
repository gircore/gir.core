namespace GtkChamplain
{
    public class Embed : Gtk.Bin
    {
        public Embed() : base(Sys.Embed.@new()) { }
    }
}
