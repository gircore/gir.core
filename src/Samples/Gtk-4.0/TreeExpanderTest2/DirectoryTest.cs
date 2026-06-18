using Gtk;

namespace Program;

[GObject.Subclass<Gtk.Box>]
partial class DirectoryTest
{
    public Gio.ListStore Store { get; } = Gio.ListStore.New(GObject.Object.GetGType());
    
    partial void Initialize()
    {
        MultiSelection model1 = MultiSelection.New(Store);
        MultiSelection model3 = MultiSelection.New(Store);
    }

    public static DirectoryTest New() => NewWithProperties([]);

    public void LoadRecords()
    {
        for (int i = 0; i < 5000; i++)
        {
            Store.Append(GObject.Object.NewWithProperties([]));
        }
    }
}