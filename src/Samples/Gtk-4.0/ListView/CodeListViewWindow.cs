using GObject;
using Gtk;
using static Gtk.SignalListItemFactory;

namespace ListViewSample;

[Subclass<Window>]
public partial class CodeListViewWindow
{
    partial void Initialize()
    {
        Title = "Code ListView";
        SetDefaultSize(300, 300);

        var stringList = StringList.New(["One", "Two", "Three", "Four"]);
        var selectionModel = NoSelection.New(stringList);
        var listItemFactory = SignalListItemFactory.New();
        listItemFactory.OnSetup += SetupSignalHandler;
        listItemFactory.OnBind += BindSignalHandler;

        var listView = ListView.New(selectionModel, listItemFactory);

        var scrolledWindow = ScrolledWindow.New();
        scrolledWindow.Child = listView;
        Child = scrolledWindow;
    }

    private static void SetupSignalHandler(SignalListItemFactory sender, SetupSignalArgs args)
    {
        if (args.Object is not ListItem listItem)
            return;

        var label = Label.New(null);
        listItem.Child = label;
    }

    private static void BindSignalHandler(SignalListItemFactory sender, BindSignalArgs args)
    {
        if (args.Object is not ListItem listItem)
            return;

        if (listItem.Child is not Label label)
            return;

        var item = listItem.Item as StringObject;
        label.SetText(item?.String ?? "NOT FOUND");
    }
}
