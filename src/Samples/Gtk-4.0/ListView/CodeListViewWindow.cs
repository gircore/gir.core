using Gtk;
using static Gtk.SignalListItemFactory;

namespace ListViewSample;

public class CodeListViewWindow : Window
{
    public CodeListViewWindow()
    {
        Title = "Code ListView";
        SetDefaultSize(300, 300);

        var stringList = StringList.New(new string[] { "One", "Two", "Three", "Four" });
        var selectionModel = NoSelection.New(stringList);
        var listItemFactory = SignalListItemFactory.New();
        listItemFactory.OnSetup += SetupSignalHandler;
        listItemFactory.OnBind += BindSignalHandler;

        var listView = ListView.New(selectionModel, listItemFactory);

        var scrolledWindow = ScrolledWindow.New();
        scrolledWindow.Child = listView;
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
