
using Gtk;

namespace Program;

class Program
{
    public static Gio.ListStore Store { get; } = Gio.ListStore.New(GObject.Object.GetGType());
    
    private static void Main()
    {
        var app = Application.New("ua.org.accounting.test", Gio.ApplicationFlags.FlagsNone);
        app.OnActivate += (_, _) =>
        {
            var button = Button.NewWithLabel("Add");
            button.OnClicked +=  (_, _) => Add();
            
            var window = Gtk.Window.New();
            window.SetDefaultSize(100, 100);
            window.Application = app;
            window.Child = button;
            window.Show();
        };

        app.RunWithSynchronizationContext(null);
    }
    
    static void Add()
    {
        MultiSelection model1 = MultiSelection.New(Store);
        MultiSelection model3 = MultiSelection.New(Store);
        
        for (int i = 0; i < 5000; i++)
        {
            Store.Append(GObject.Object.NewWithProperties([]));
        }
    }
}
