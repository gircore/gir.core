using System;

namespace BuilderSample;

[GObject.Subclass<Gtk.Dialog>]
public partial class SampleTestDialog
{
    private Gtk.Button? _okButton;

    public void Connect(Gtk.Builder builder)
    {
        _okButton = (Gtk.Button) (builder.GetObject("okButton") ?? throw new Exception("OK Button not found in xml"));
        _okButton.OnClicked += (_, _) => this.Response((int) Gtk.ResponseType.Ok);
    }
}
