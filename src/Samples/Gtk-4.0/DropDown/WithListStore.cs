namespace DropDown;

[GObject.Subclass<Gtk.ApplicationWindow>]
public partial class WithListStore
{
    private Gtk.Label _labelSelected;
    private Gtk.DropDown _dropDown;
    private Gio.ListStore _listStore;

    partial void Initialize()
    {
        Title = "DropDown With List Store";
        SetDefaultSize(300, 300);

        _labelSelected = Gtk.Label.New("Selected title:");
        _labelSelected.SetMarginTop(12);
        _labelSelected.SetMarginBottom(12);
        _labelSelected.SetMarginStart(12);
        _labelSelected.SetMarginEnd(12);

        _dropDown = new Gtk.DropDown();
        _dropDown.SetMarginTop(12);
        _dropDown.SetMarginBottom(12);
        _dropDown.SetMarginStart(12);
        _dropDown.SetMarginEnd(12);

        // Factory for presenting the selected item.
        var selectedFactory = Gtk.SignalListItemFactory.New();
        selectedFactory.OnSetup += OnSetupSelectedItem;
        selectedFactory.OnBind += OnBindSelectedItem;
        _dropDown.SetFactory(selectedFactory);

        // Factory for presenting the items in the dropdown list.
        var listFactory = Gtk.SignalListItemFactory.New();
        listFactory.OnSetup += OnSetupListItem;
        listFactory.OnBind += OnBindListItem;
        _dropDown.SetListFactory(listFactory);

        CreateModel();
        _dropDown.SetModel(_listStore);
        _dropDown.SetSelected(0);

        _dropDown.OnNotify += OnDropDownChanged;

        var gtkBox = Gtk.Box.New(Gtk.Orientation.Vertical, 0);
        gtkBox.Append(_labelSelected);
        gtkBox.Append(_dropDown);

        Child = gtkBox;
    }

    private void OnDropDownChanged(GObject.Object sender, NotifySignalArgs args)
    {
        var dropDown = sender as Gtk.DropDown;
        var selectedItem = dropDown?.SelectedItem as StringHolder;
        var title = selectedItem?.Title;
        _labelSelected.SetLabel($"Selected title:  {title}");
    }

    private void CreateModel()
    {
        _listStore = Gio.ListStore.New(StringHolder.GetGType());
        _listStore.Append(new StringHolder("Deskop", "user-desktop-symbolic", "Deskop Folder"));
        _listStore.Append(new StringHolder("Home", "user-home-symbolic", "Home Folder"));
        _listStore.Append(new StringHolder("Trash", "user-trash-symbolic", "Trash Folder"));
        _listStore.Append(new StringHolder("Videos", "folder-videos-symbolic", "Videos Folder"));
    }

    private static void OnSetupSelectedItem(Gtk.SignalListItemFactory factory, Gtk.SignalListItemFactory.SetupSignalArgs args)
    {
        var listItem = args.Object as Gtk.ListItem;
        var box = Gtk.Box.New(Gtk.Orientation.Horizontal, 10);
        box.Append(Gtk.Image.New());
        box.Append(Gtk.Label.New(""));
        listItem!.SetChild(box);
    }

    private static void OnBindSelectedItem(Gtk.SignalListItemFactory sender, Gtk.SignalListItemFactory.BindSignalArgs args)
    {
        var listItem = args.Object as Gtk.ListItem;
        var stringHolder = listItem!.GetItem() as StringHolder;
        if (stringHolder is null) return;

        var box = listItem.GetChild() as Gtk.Box;
        if (box is null) return;

        var image = box.GetFirstChild() as Gtk.Image;
        if (image is null) return;

        image.SetFromIconName(stringHolder.Icon);
        var label = image.GetNextSibling() as Gtk.Label;
        if (label is null) return;

        label.SetText(stringHolder.Title);
    }

    private static void OnSetupListItem(Gtk.SignalListItemFactory factory, Gtk.SignalListItemFactory.SetupSignalArgs args)
    {
        var listItem = args.Object as Gtk.ListItem;

        var hbox = Gtk.Box.New(Gtk.Orientation.Horizontal, 10);
        var vbox = Gtk.Box.New(Gtk.Orientation.Vertical, 2);

        hbox.Append(Gtk.Image.New());
        hbox.Append(vbox);

        var lblTitle = Gtk.Label.New("");
        lblTitle.SetXalign(0);
        vbox.Append(lblTitle);

        var lblDescription = Gtk.Label.New("");
        lblDescription.SetXalign(0);
        lblDescription.SetCssClasses(["dim-label"]);
        vbox.Append(lblDescription);

        var checkmark = Gtk.Image.New();
        checkmark.SetFromIconName("object-select-symbolic");
        checkmark.SetVisible(false);
        hbox.Append(checkmark);

        listItem!.SetChild(hbox);
    }

    private void OnBindListItem(Gtk.SignalListItemFactory sender, Gtk.SignalListItemFactory.BindSignalArgs args)
    {
        var listItem = args.Object as Gtk.ListItem;

        var stringHolder = listItem!.GetItem() as StringHolder;
        if (stringHolder is null) return;

        var hbox = listItem.GetChild() as Gtk.Box;
        if (hbox is null) return;

        var image = hbox.GetFirstChild() as Gtk.Image;
        image?.SetFromIconName(stringHolder.Icon);

        var vbox = image?.GetNextSibling() as Gtk.Box;
        if (vbox is null) return;

        var title = vbox.GetFirstChild() as Gtk.Label;
        title?.SetText(stringHolder.Title);
        var description = title?.GetNextSibling() as Gtk.Label;
        description?.SetText(stringHolder.Description);

        Gtk.DropDown.SelectedPropertyDefinition.Notify(_dropDown, (_, _) =>
        {
            OnSelectedItemChanged(listItem);
        });

        listItem.SetData("connection", IntPtr.Zero);
        OnSelectedItemChanged(listItem);
    }

    private void OnSelectedItemChanged(Gtk.ListItem listItem)
    {
        var hbox = listItem.GetChild() as Gtk.Box;
        if (hbox is null) return;

        var checkmark = hbox.GetLastChild() as Gtk.Image;
        if (checkmark is null) return;

        checkmark.SetVisible(_dropDown.GetSelectedItem() == listItem.Item);
    }
}

[GObject.Subclass<GObject.Object>]
public partial class StringHolder
{
    public StringHolder(string title, string icon, string description) : this()
    {
        Title = title;
        Icon = icon;
        Description = description;
    }

    public string Title { get; }
    public string Icon { get; }
    public string Description { get; }
}
