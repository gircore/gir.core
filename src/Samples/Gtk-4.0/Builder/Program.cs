var application = Gtk.Application.New("org.gir.core", Gio.ApplicationFlags.FlagsNone);
application.OnActivate += (sender, args) =>
{
    var window = Gtk.ApplicationWindow.New((Gtk.Application) sender);
    
    var button1 = Gtk.Button.NewWithLabel("Classic Gtk.Builder");
    button1.OnClicked += (button, eventArgs) =>
    {
        var dialog = new BuilderSample.SampleTestDialog();
        dialog.Application = (Gtk.Application)sender;
        dialog.TransientFor = window;
        dialog.OnResponse += (_, _) => dialog.Close();
        dialog.Show();
    };
    
    var button2 = Gtk.Button.NewWithLabel("New use of Gtk.Builder");
    button2.OnClicked += (button, eventArgs) =>
    {
        var type = BuilderSample.SampleTestDialog2.GetGType(); //TODO: Register type before builder knows about it
        var builder = new Gtk.Builder("SampleTestDialog2.ui");
        var dialog = (BuilderSample.SampleTestDialog2?) builder.GetObject("dialog") ?? throw new System.Exception("Couldnot get Object");
        dialog.Build(builder);
        dialog.Application = (Gtk.Application)sender;
        dialog.TransientFor = window;
        dialog.OnResponse += (_, _) => dialog.Close();
        dialog.Show();
        
        //TODO: For deeper Gtk.Builder integration there is probably Gtk.BuilderScope support needed or some manual source generation from dotnet which
        //listens to attributes.
    };
    
    var box = Gtk.Box.New(Gtk.Orientation.Vertical, 0);
    box.Append(button1);
    box.Append(button2);
   
    window.Title = "GtkBuilder Sample";
    window.SetDefaultSize(300, 300);
    window.SetChild(box);
    window.Show();
};

return application.RunWithSynchronizationContext(null);
