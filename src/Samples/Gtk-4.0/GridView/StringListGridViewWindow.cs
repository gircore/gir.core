using Gtk;
using static Gtk.SignalListItemFactory;

namespace GridViewSample;

public class StringListGridViewWindow : Window
{
    public StringListGridViewWindow()
    {
        Title = "Gtk::GridView (Gio::ListStore)";
        SetDefaultSize(400, 400);

        var stringList = StringList.New(["One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten"]);
        var selectionModel = NoSelection.New(stringList);
        var listItemFactory = SignalListItemFactory.New();
        listItemFactory.OnSetup += SetupSignalHandler;
        listItemFactory.OnBind += BindSignalHandler;

        var gridView = GridView.New(selectionModel, listItemFactory);

        var scrolledWindow = ScrolledWindow.New();
        scrolledWindow.Child = gridView;
        Child = scrolledWindow;
    }

    private void SetupSignalHandler(SignalListItemFactory sender, SetupSignalArgs args)
    {
        var listItem = args.Object as ListItem;
        if (listItem is null)
        {
            return;
        }

        var label = Label.New(null);
        label.WidthRequest = 100;
        label.HeightRequest = 100;
        listItem.Child = label;
    }

    private void BindSignalHandler(SignalListItemFactory sender, BindSignalArgs args)
    {
        var listItem = args.Object as ListItem;
        if (listItem is null)
        {
            return;
        }

        var label = listItem.Child as Label;
        if (label is null)
        {
            return;
        }

        var item = listItem.Item as StringObject;
        label.SetText(item?.String ?? "NOT FOUND");
    }
}
