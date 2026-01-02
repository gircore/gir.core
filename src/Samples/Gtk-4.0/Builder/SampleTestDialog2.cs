using System;

namespace BuilderSample;

[GObject.Subclass<Gtk.Dialog>]
public partial class SampleTestDialog2
{
    private Gtk.Button? _okButton;

    internal void Build(Gtk.Builder builder)
    {
        _okButton = (Gtk.Button?) builder.GetObject("okButton") ?? throw new Exception("Could not get OK button");
        _okButton.OnClicked += (_, _) => this.Response((int) Gtk.ResponseType.Ok);
    }
}
