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

        public DemoWindow(Application application) : this(application, new Builder("demo_window.glade")) { }
        private DemoWindow(Application application, Builder builder) : base(builder.GetObject("root"))
        {
            Application = application;
            builder.Connect(this);
            
            // Connect Button
            Button.OnClicked += Button_Clicked;

            // Create Notebook
            notebook = new Notebook();
            Box.PackStart(notebook, true, true, 0);

            var image = Image.NewFromFile("data/gtk.png");
            notebook.InsertPage("Image", image, 0);

            var label = new Label("Gtk and C# - Very exciting isn't it?");
            notebook.InsertPage("Label", label, 1);

            var button = new Button("Open!");
            notebook.InsertPage("Dialogue", button, 2);
            button.OnClicked += OpenDialog;
        }

        private void Button_Clicked(Button sender, EventArgs args)
        {
            Console.WriteLine("Hello World!");
            Resizable = !Resizable;
        }

        private void OpenDialog(Button sender, EventArgs args)
        {
            // TODO: Investigate adding widgets to GDialog
            var dialog = new Dialog();
            dialog.Run();
        }
    }
}
