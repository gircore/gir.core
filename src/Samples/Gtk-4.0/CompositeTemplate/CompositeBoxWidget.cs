namespace CompositeTemplate;

[GObject.Subclass<Gtk.Box>]
[Gtk.Template<Gtk.AssemblyResource>("CompositeBoxWidget.ui")]
public partial class CompositeBoxWidget
{
    [Gtk.Connect("my_label")]
    private Gtk.Label _label;

    public static CompositeBoxWidget New()
    {
        return NewWithProperties([]);
    }

    partial void Initialize()
    {
        _label.Label_ = "With support for connected members!";
    }
}
