using Gtk;

namespace BuilderSample;

public class SampleTestDialog : Dialog
{
    [Connect] private readonly Button okButton;

    private SampleTestDialog(Builder builder, string name) : base(builder.GetPointer(name), false)
    {
        builder.Connect(this);

        okButton.OnClicked += (_, _) => this.Response((int) ResponseType.Ok);
    }

    public SampleTestDialog() : this(new Builder("SampleTestDialog.4.ui"), "dialog")
    {
    }
}
