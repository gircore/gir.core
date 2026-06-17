using Gtk;

namespace Program;

[GObject.Subclass<Gtk.Box>]
partial class DirectoryTest
{
    TreeListModel? TreeList { get; set; } = null;
    public ColumnView Grid { get; } = ColumnView.NewWithProperties([]);
    protected ScrolledWindow ScrollGrid { get; } = ScrolledWindow.New();
    protected Box HBoxTop { get; } = Box.New(Orientation.Horizontal, 0);
    public Gio.ListStore Store { get; } = Gio.ListStore.New(DirectoryHierarchicalRow.GetGType());
    
    partial void Initialize()
    {
        Console.WriteLine("DirectoryTest: " + GetType());

        SetOrientation(Orientation.Vertical);
        ScrollGrid.SetPolicy(PolicyType.Automatic, PolicyType.Automatic);

        Append(HBoxTop);

        MultiSelection model1 = MultiSelection.New(Store);
        Grid.Model = model1;

        ScrollGrid.SetChild(Grid);
        ScrollGrid.Vexpand = ScrollGrid.Hexpand = true;

        Append(ScrollGrid);
        
        //Модель для дерева
        TreeList = TreeListModel.New(Store, false, false, CreateFunc);

        //Модель
        MultiSelection model2 = MultiSelection.New(TreeList);
        Grid.Model = model2;
    }

    public static DirectoryTest New() => NewWithProperties([]);

    public DirectoryHierarchicalRow LoadEmptyChildren()
    {
        DirectoryHierarchicalRow row = DirectoryHierarchicalRow.New();
        row.Fields.Add("Name", null);

        return row;
    }

    public async Task<List<DirectoryHierarchicalRow>> LoadChildren()
    {
        List<DirectoryHierarchicalRow> list = [];

        for (int i = 0; i < 100; i++)
        {
            DirectoryHierarchicalRow row = DirectoryHierarchicalRow.New();
            row.Fields.Add("Name", "Name name");

            list.Add(row);
        }

        return list;
    }

    public void LoadRecords()
    {
        Console.WriteLine("LoadRecords");

        Store.RemoveAll();

        for (int i = 0; i < 5000; i++)
        {
            DirectoryHierarchicalRow row = DirectoryHierarchicalRow.New();
            row.Fields.Add("Name", $"Name {i}");

            Store.Append(row);
        }
    }
    
    private Gio.ListModel? CreateFunc(GObject.Object item)
    {
        DirectoryHierarchicalRow itemRow = (DirectoryHierarchicalRow)item;
        Gio.ListStore? store = null;

        if (itemRow.IsLoading)
        {
            if (itemRow.Store != null)
            {
                GLib.Functions.IdleAdd(0, () =>
                {
                    async void f()
                    {
                        await Task.Delay(500);

                        try
                        {
                            itemRow.Store.RemoveAll();

                            var list = await LoadChildren();
                            foreach (var item in list)
                                itemRow.Store.Append(item);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    }

                    f();

                    return false;
                });
            }
        }
        else
        {
            store = Gio.ListStore.New(DirectoryHierarchicalRow.GetGType());

            DirectoryHierarchicalRow itemEmpty = LoadEmptyChildren();
            itemEmpty.IsLoading = true;
            itemEmpty.Store = store;

            store.Append(itemEmpty);
        }

        return store;
    }
}