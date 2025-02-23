using System.Reflection;
using GLib;
using GObject;
using Gtk;

namespace ListViewSample;

public class TemplateListViewWindow : Window
{
    public TemplateListViewWindow()
    {
        Title = "Template ListView";
        SetDefaultSize(300, 300);

        var stringList = StringList.New(["One", "Two", "Three", "Four"]);
        var selectionModel = SingleSelection.New(stringList);
        var bytes = Assembly.GetExecutingAssembly()
            .ReadResourceAsByteArray("ListItemTemplate.ui");
        var listItemFactory = BuilderListItemFactory.NewFromBytes(null, Bytes.New(bytes));
        var listView = ListView.New(selectionModel, listItemFactory);

        var scrolledWindow = ScrolledWindow.New();
        scrolledWindow.Child = listView;
        Child = scrolledWindow;
    }
}
