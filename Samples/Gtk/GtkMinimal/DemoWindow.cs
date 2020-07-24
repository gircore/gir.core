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

        public DemoWindow(GApplication application) : base(application, "ui.glade")
        {
            // Connect Button
            Button.Clicked += Button_Clicked;

            // Create Notebook
            notebook = new GNotebook();
            Box.PackStart(notebook, true, true, 0);

            FileImage image = new FileImage("data/gtk.png");
            notebook.InsertPage("Image", image, 0);

            GLabel label = new GLabel("Gtk and C# - Very exciting isn't it?");
            notebook.InsertPage("Label", label, 1);

            GButton button = new GButton("Open!");
            notebook.InsertPage("Dialogue", button, 2);
            button.Clicked += OpenDialog;
        }

        private void Button_Clicked(object? sender, EventArgs args)
        {
            Console.WriteLine("Hello World!");
        }

        private void OpenDialog(object? sender, EventArgs args)
        {
            // TODO: This doesn't seem to work?
            GDialog dialog = new GDialog();
            GContainer contentArea = dialog.ContentArea;
            GLabel label = new GLabel("Boo!");
            contentArea.Add(label);
            dialog.Run();
        }
    }
}