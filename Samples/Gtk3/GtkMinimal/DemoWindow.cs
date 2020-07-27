using System;
using Gtk.Core;

namespace GtkDemo
{
    public class DemoWindow : GApplicationWindow
    {
        [Connect]
        private GButton Button = default!;
        
        [Connect]
        private GBox Box = default!;

        private GNotebook notebook;

        public DemoWindow(GApplication application) : base(application, "demo_window.glade")
        {
            // Connect Button
            Button.Clicked += Button_Clicked;

            // Create Notebook
            notebook = new GNotebook();
            Box.PackStart(notebook, true, true, 0);

            var image = new FileImage("data/gtk.png");
            notebook.InsertPage("Image", image, 0);

            var label = new GLabel("Gtk and C# - Very exciting isn't it?");
            notebook.InsertPage("Label", label, 1);

            var button = new GButton("Open!");
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
            var dialog = new GDialog();
            dialog.Run();
        }
    }
}