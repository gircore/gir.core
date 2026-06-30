namespace CompositeTemplate;

[GObject.Subclass<Gtk.Label>(qualifiedName: nameof(CompositeLabelWidget))]
[Gtk.Template<Gtk.GResource>("/org/gir/core/CompositeLabelWidget.ui")]
public partial class CompositeLabelWidget
{
    partial void Initialize()
    {
        Label_ = "…and loading templates from GResources!";
    }
}
