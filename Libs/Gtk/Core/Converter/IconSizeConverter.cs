namespace Gtk.Core
{
    public static class IconSizeConverter
    {
        public static Gtk.IconSize ToGtk(this IconSize i) => i switch
        {
            IconSize.Button => Gtk.IconSize.button,
            IconSize.Dialog => Gtk.IconSize.dialog,
            IconSize.Dnd => Gtk.IconSize.dialog,
            IconSize.Invalid => Gtk.IconSize.invalid,
            IconSize.LargeToolbar => Gtk.IconSize.large_toolbar,
            IconSize.Menu => Gtk.IconSize.menu,
            IconSize.SmallToolbar => Gtk.IconSize.small_toolbar,
            _ => throw new System.Exception($"Unknown value of {nameof(IconSize)}")
        };

        public static IconSize FromGtk(this Gtk.IconSize e) => e switch
        {
            Gtk.IconSize.button => IconSize.Button,
            Gtk.IconSize.dialog => IconSize.Dialog,
            Gtk.IconSize.dnd => IconSize.Dnd,
            Gtk.IconSize.invalid => IconSize.Invalid,
            Gtk.IconSize.large_toolbar => IconSize.LargeToolbar,
            Gtk.IconSize.menu => IconSize.Menu,
            Gtk.IconSize.small_toolbar => IconSize.SmallToolbar,
            _ => throw new System.Exception($"Unknown value of {nameof(Gtk)}.{nameof(IconSize)}")
        };
    }
}