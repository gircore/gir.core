var application = Gtk.Application.New("org.gir.core", Gio.ApplicationFlags.FlagsNone);

application.OnActivate += (sender, args) =>
{
    var listItemFactory = Gtk.SignalListItemFactory.New();
    listItemFactory.OnSetup += Setup;
    listItemFactory.OnBind += Bind;

    var sorter = Gtk.CustomSorter.New<Data>((a, b) =>
    {
        return string.Compare(a.Number, b.Number, StringComparison.Ordinal);
    });

    var column = Gtk.ColumnViewColumn.New("Number", listItemFactory);
    column.SetSorter(sorter);

    var columnView = Gtk.ColumnView.New(null);
    columnView.AppendColumn(column);

    var data = Gio.ListStore.New(Data.GetGType());
    data.Append(new Data { Number = "One" });
    data.Append(new Data { Number = "Two" });

    columnView.SetModel(
        model: Gtk.SingleSelection.New(
            model: Gtk.SortListModel.New(
                model: data,
                sorter: columnView.GetSorter()
    )));

    var box = Gtk.CenterBox.New();
    box.SetCenterWidget(columnView);

    var window = Gtk.ApplicationWindow.New((Gtk.Application) sender);
    window.Title = "Sorted ListView Sample";
    window.SetDefaultSize(300, 300);
    window.Child = box;
    window.Show();
};

return application.RunWithSynchronizationContext(null);

void Bind(Gtk.SignalListItemFactory signalListItemFactory, Gtk.SignalListItemFactory.BindSignalArgs bindSignalArgs)
{
    var listItem = (Gtk.ListItem) bindSignalArgs.Object;
    var data = (Data?) listItem.Item ?? throw new Exception("No Item available");
    var label = (Gtk.Label?) listItem.Child ?? throw new Exception("No child available");
    label.SetText(data.Number ?? "Empty");
}

void Setup(Gtk.SignalListItemFactory signalListItemFactory, Gtk.SignalListItemFactory.SetupSignalArgs setupSignalArgs)
{
    var listItem = (Gtk.ListItem) setupSignalArgs.Object;
    listItem.SetChild(Gtk.Label.New(null));
}

[GObject.Subclass<GObject.Object>]
public partial class Data
{
    public string? Number { get; set; }
}
