namespace CompositeTemplate;

[GObject.Subclass<Gtk.Box>(qualifiedName: nameof(CompositeBoxWidget))]
[Gtk.Template<Gtk.AssemblyResource>("CompositeBoxWidget.ui")]
public partial class CompositeBoxWidget
{
    [Gtk.Connect("my_label")]
    private Gtk.Label _label;

    partial void Initialize()
    {
        _label.Label_ = "With support for connected members!";
    }
}
