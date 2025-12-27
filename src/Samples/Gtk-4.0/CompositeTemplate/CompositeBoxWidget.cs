using System.Reflection;
using GObject;

namespace CompositeTemplate;

[GObject.Subclass<Gtk.Box>]
[Gtk.Template("file.ui")]
public partial class CompositeBoxWidget
{
    public void Bla()
    {

    }
}
