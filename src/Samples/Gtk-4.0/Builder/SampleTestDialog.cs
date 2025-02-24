namespace BuilderSample;

public class SampleTestDialog : Gtk.Dialog
{
    [Gtk.Connect] private readonly Gtk.Button okButton;

    private SampleTestDialog(Gtk.Builder builder, string name) : base(new Gtk.Internal.DialogHandle(builder.GetPointer(name), false))
    {
        builder.Connect(this);

        okButton.OnClicked += (_, _) => this.Response((int) Gtk.ResponseType.Ok);
    }

    public SampleTestDialog() : this(new Gtk.Builder("SampleTestDialog.4.ui"), "dialog")
    {
    }
}
