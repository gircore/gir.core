using System.Diagnostics.CodeAnalysis;

namespace DropDown;

[GObject.Subclass<Gtk.ApplicationWindow>]
public partial class WithStringList
{
    private Gtk.Label _labelSelected;
    private Gtk.DropDown _dropDown;

    [MemberNotNull(nameof(_labelSelected), nameof(_dropDown))]
    partial void Initialize()
    {
        Title = "DropDown With StringList";
        SetDefaultSize(300, 300);

        _labelSelected = Gtk.Label.New("Selected: 1 minute");
        _labelSelected.SetMarginTop(12);
        _labelSelected.SetMarginBottom(12);
        _labelSelected.SetMarginStart(12);
        _labelSelected.SetMarginEnd(12);

        var stringList = Gtk.StringList.New(["1 minute", "2 minutes", "5 minutes", "15 minutes", "30 minutes"]);
        _dropDown = Gtk.DropDown.New(null, null);
        _dropDown.SetModel(stringList);
        _dropDown.SetSelected(0);
        _dropDown.SetMarginTop(12);
        _dropDown.SetMarginBottom(12);
        _dropDown.SetMarginStart(12);
        _dropDown.SetMarginEnd(12);

        _dropDown.OnNotify += (_, _) =>
        {
            var selectedItem = (Gtk.StringObject) _dropDown.SelectedItem!;
            var interval = selectedItem.GetString();
            _labelSelected.SetLabel($"Selected: {interval}");
        };

        var gtkBox = Gtk.Box.New(Gtk.Orientation.Vertical, 0);
        gtkBox.Append(_labelSelected);
        gtkBox.Append(_dropDown);

        Child = gtkBox;
    }
}
