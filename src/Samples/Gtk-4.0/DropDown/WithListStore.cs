using System.Diagnostics.CodeAnalysis;

namespace DropDown;

[GObject.Subclass<Gtk.ApplicationWindow>]
public partial class WithListStore
{
    private Gtk.Label _labelSelected;
    private MyDropDown _dropDown;

    [MemberNotNull(nameof(_labelSelected), nameof(_dropDown))]
    partial void Initialize()
    {
        Title = "DropDown With List Store";
        SetDefaultSize(300, 300);

        _labelSelected = Gtk.Label.New("Selected title:");
        _labelSelected.SetMarginTop(12);
        _labelSelected.SetMarginStart(12);
        _labelSelected.SetMarginEnd(12);

        _dropDown = MyDropDown.NewWithProperties([]);
        _dropDown.OnNotify += OnDropDownChanged;
        _dropDown.SetMarginStart(12);
        _dropDown.SetMarginEnd(12);

        var gtkBox = Gtk.Box.New(Gtk.Orientation.Vertical, 12);
        gtkBox.Append(_labelSelected);
        gtkBox.Append(_dropDown);

        Child = gtkBox;
    }

    private void OnDropDownChanged(GObject.Object sender, NotifySignalArgs args)
    {
        var dropDown = sender as MyDropDown;
        var selectedItem = dropDown?.SelectedItem as DropDownData;
        var title = selectedItem?.Title;
        _labelSelected.SetLabel($"Selected title: {title}");
    }
}

[GObject.Subclass<Gtk.DropDown>]
public partial class MyDropDown
{
    partial void Initialize()
    {
        // Factory for presenting the selected item.
        var selectedFactory = Gtk.SignalListItemFactory.New();
        selectedFactory.OnSetup += OnSetupSelectedItem;
        selectedFactory.OnBind += OnBindSelectedItem;
        SetFactory(selectedFactory);

        // Factory for presenting the items in the dropdown list.
        var listFactory = Gtk.SignalListItemFactory.New();
        listFactory.OnSetup += OnSetupListItem;
        listFactory.OnBind += OnBindListItem;
        SetListFactory(listFactory);

        SetModel(CreateModel());
    }

    private static Gio.ListStore CreateModel()
    {
        var listStore = Gio.ListStore.New<DropDownData>();
        listStore.Append(DropDownData.New("Deskop", "user-desktop-symbolic", "Deskop Folder"));
        listStore.Append(DropDownData.New("Home", "user-home-symbolic", "Home Folder"));
        listStore.Append(DropDownData.New("Trash", "user-trash-symbolic", "Trash Folder"));
        listStore.Append(DropDownData.New("Videos", "folder-videos-symbolic", "Videos Folder"));

        return listStore;
    }

    private static void OnSetupSelectedItem(Gtk.SignalListItemFactory factory, Gtk.SignalListItemFactory.SetupSignalArgs args)
    {
        var listItem = args.Object as Gtk.ListItem;
        listItem?.SetChild(SelectedDropDownItem.NewWithProperties([]));
    }

    private static void OnBindSelectedItem(Gtk.SignalListItemFactory sender, Gtk.SignalListItemFactory.BindSignalArgs args)
    {
        var listItem = args.Object as Gtk.ListItem;
        if (listItem?.GetItem() is not DropDownData data)
            return;

        var item = listItem.GetChild() as SelectedDropDownItem;
        item?.Bind(data);
    }

    private static void OnSetupListItem(Gtk.SignalListItemFactory factory, Gtk.SignalListItemFactory.SetupSignalArgs args)
    {
        var listItem = args.Object as Gtk.ListItem;
        listItem?.SetChild(DropDownItem.NewWithProperties([]));
    }

    private void OnBindListItem(Gtk.SignalListItemFactory sender, Gtk.SignalListItemFactory.BindSignalArgs args)
    {
        var listItem = args.Object as Gtk.ListItem;
        if (listItem?.GetItem() is not DropDownData data)
            return;

        var item = listItem.GetChild() as DropDownItem;
        item?.Bind(data);

        SelectedItemPropertyDefinition.Notify(this, (_, _) =>
        {
            OnSelectedItemChanged(listItem);
        });

        OnSelectedItemChanged(listItem);
    }

    private void OnSelectedItemChanged(Gtk.ListItem listItem)
    {
        var item = listItem.GetChild() as DropDownItem;
        item?.SetCheckmarkVisible(GetSelectedItem() == listItem.Item);
    }

    [GObject.Subclass<Gtk.Box>]
    internal partial class SelectedDropDownItem
    {
        private readonly Gtk.Image _image = Gtk.Image.New();
        private readonly Gtk.Label _title = Gtk.Label.New("");

        partial void Initialize()
        {
            SetOrientation(Gtk.Orientation.Horizontal);
            SetSpacing(10);

            Append(_image);
            Append(_title);
        }

        public void Bind(DropDownData dropDownData)
        {
            _image.SetFromIconName(dropDownData.Icon);
            _title.SetText(dropDownData.Title ?? string.Empty);
        }
    }

    [GObject.Subclass<Gtk.Box>]
    internal partial class DropDownItem
    {
        private readonly Gtk.Image _image = Gtk.Image.New();
        private readonly Gtk.Label _title = Gtk.Label.New("");
        private readonly Gtk.Label _description = Gtk.Label.New("");
        private readonly Gtk.Image _checkmark = Gtk.Image.NewFromIconName("object-select-symbolic");

        partial void Initialize()
        {
            SetOrientation(Gtk.Orientation.Horizontal);
            SetSpacing(10);
            ConfigureWidgets();

            Append(_image);
            Append(CreateVBox());
            Append(_checkmark);
        }

        private void ConfigureWidgets()
        {
            _title.SetXalign(0);
            _description.SetXalign(0);
            _description.SetCssClasses(["dim-label"]);
            _checkmark.SetVisible(false);
        }

        private Gtk.Box CreateVBox()
        {
            var vbox = Gtk.Box.New(Gtk.Orientation.Vertical, 2);
            vbox.Append(_title);
            vbox.Append(_description);
            return vbox;
        }

        public void Bind(DropDownData dropDownData)
        {
            _image.SetFromIconName(dropDownData.Icon);
            _title.SetText(dropDownData.Title ?? string.Empty);
            _description.SetText(dropDownData.Description ?? string.Empty);
        }

        public void SetCheckmarkVisible(bool visible)
        {
            _checkmark.SetVisible(visible);
        }
    }
}

[GObject.Subclass<GObject.Object>]
public partial class DropDownData
{
    public static DropDownData New(string title, string icon, string description)
    {
        var obj = NewWithProperties([]);
        obj.Title = title;
        obj.Icon = icon;
        obj.Description = description;

        return obj;
    }

    public string? Title { get; private set; }
    public string? Icon { get; private set; }
    public string? Description { get; private set; }
}
