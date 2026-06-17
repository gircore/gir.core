
using Gtk;

namespace Program;

class Program
{
    private static readonly Application BasicApp = Application.New("ua.org.accounting.test", Gio.ApplicationFlags.FlagsNone);

    private static void Main()
    {
        BasicApp.OnActivate += (app, args) =>
        {
            var window = FormWindow.New();
            window.Application = BasicApp;
            window.Show();
        };

        BasicApp.RunWithSynchronizationContext(null);
    }
}
