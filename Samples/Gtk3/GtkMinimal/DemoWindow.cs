using System;
using Gtk;

namespace GtkDemo
{
    public class DemoWindow : ApplicationWindow
    {
        [Connect]
        private Button Button = default!;
        
        [Connect]
        private Box Box = default!;

        private Notebook notebook;

        public DemoWindow(Application application) : base(application, "demo_window.glade")
        {
            // Connect Button
            Button.Clicked += Button_Clicked;

            // Create Notebook
            notebook = new Notebook();
            Box.PackStart(notebook, true, true, 0);

            var image = new FileImage("data/gtk.png");
            notebook.InsertPage("Image", image, 0);

            var label = new Label("Gtk and C# - Very exciting isn't it?");
            notebook.InsertPage("Label", label, 1);

            var button = new Button("Open!");
            notebook.InsertPage("Dialogue", button, 2);
            button.Clicked += OpenDialog;
        }

        private void Button_Clicked(object? sender, EventArgs args)
        {
            Console.WriteLine("Hello World!");
        }

        private void OpenDialog(object? sender, EventArgs args)
        {
            // TODO: Investigate adding widgets to GDialog
            var dialog = new Dialog();
            dialog.Run();
        }
    }
}