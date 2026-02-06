using System;
using GObject;
using Gtk;
using static Gtk.GridView;
using static Gtk.SignalListItemFactory;
using ListStore = Gio.ListStore;

namespace GridViewSample;

[Subclass<GObject.Object>]
public partial class ItemData
{
    public string? ImagePath { get; private set; }
    public string? Text { get; private set; }
    public string? Description { get; private set; }

    public static ItemData New(string imagePath, string text, string description)
    {
        var obj = NewWithProperties([]);
        obj.ImagePath = imagePath;
        obj.Text = text;
        obj.Description = description;

        return obj;
    }
}

[Subclass<Window>]
public partial class CustomObjectGridViewWindow
{
    private readonly ListStore _model = ListStore.New(ItemData.GetGType());

    partial void Initialize()
    {
        Title = "Gtk::GridView (Gio::ListStore)";
        SetDefaultSize(400, 400);

        _model.Append(ItemData.New("Resources/number-1.svg", "One", "One"));
        _model.Append(ItemData.New("Resources/number-2.svg", "Two", "Two"));
        _model.Append(ItemData.New("Resources/number-3.svg", "Three", "Three"));
        _model.Append(ItemData.New("Resources/number-4.svg", "Four", "Four"));
        _model.Append(ItemData.New("Resources/number-5.svg", "Five", "Five"));
        _model.Append(ItemData.New("Resources/number-6.svg", "Six", "Six"));
        _model.Append(ItemData.New("Resources/number-7.svg", "Seven", "Seven"));
        _model.Append(ItemData.New("Resources/number-8.svg", "Eight", "Eight"));
        _model.Append(ItemData.New("Resources/number-9.svg", "Nine", "Nine"));

        var selectionModel = SingleSelection.New(_model);
        var listItemFactory = SignalListItemFactory.New();
        listItemFactory.OnSetup += SetupSignalHandler;
        listItemFactory.OnBind += BindSignalHandler;

        var gridView = GridView.New(selectionModel, listItemFactory);
        gridView.OnActivate += OnGridViewOnActiveHandler;

        var scrolledWindow = ScrolledWindow.New();
        scrolledWindow.Child = gridView;
        Child = scrolledWindow;
    }

    private void SetupSignalHandler(SignalListItemFactory sender, SetupSignalArgs args)
    {
        if (args.Object is not ListItem listItem)
        {
            return;
        }

        var box = Box.New(Orientation.Vertical, 2);
        box.SetSizeRequest(100, 100);
        listItem.Child = box;

        var image = Image.New();
        box.Append(image);
        var label = Label.New(null);
        box.Append(label);
    }

    private void BindSignalHandler(SignalListItemFactory sender, BindSignalArgs args)
    {
        if (args.Object is not ListItem listItem)
        {
            return;
        }

        if (listItem.Child is not Box box) return;
        if (box.GetFirstChild() is not Image image) return;
        if (image.GetNextSibling() is not Label label) return;
        if (listItem.Item is not ItemData itemData) return;

        image.SetFromFile(itemData.ImagePath);
        label.SetText(itemData.Text ?? string.Empty);
    }

    private void OnGridViewOnActiveHandler(GridView sender, ActivateSignalArgs args)
    {
        var itemData = _model.GetObject(args.Position) as ItemData;
        if (itemData is null) return;

        Console.WriteLine($"Selected Text: {itemData.Text}, Description: {itemData.Description}");
    }
}
