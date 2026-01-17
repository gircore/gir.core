namespace CompositeTemplate;

[GObject.Subclass<Gtk.Box>]
[Gtk.Template<Gtk.AssemblyResource>("CompositeBoxWidget.ui")]
public partial class CompositeBoxWidget
{
    [Gtk.Connect("my_label")]
    private Gtk.Label _label;

    partial void Initialize()
    {
        _label.Label_ = "With support for connected members!";
    }
    
    //TODO: Ensure instance is only created once in both cases:
    //- Manual call of "new CompositeBoxWidget()"
    //-Automatic creation of an instance via "InstanceInit" only
}
