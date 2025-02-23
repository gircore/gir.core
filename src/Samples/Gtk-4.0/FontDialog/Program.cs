using System;

var application = Gtk.Application.New("org.gir.core", Gio.ApplicationFlags.FlagsNone);
application.OnActivate += (sender, args) =>
{
    var labelSelectedFont = Gtk.Label.New("");
    labelSelectedFont.SetMarginTop(12);
    labelSelectedFont.SetMarginBottom(12);
    labelSelectedFont.SetMarginStart(12);
    labelSelectedFont.SetMarginEnd(12);

    var buttonSelectFont = Gtk.Button.New();
    buttonSelectFont.Label = "Select Font";
    buttonSelectFont.SetMarginTop(12);
    buttonSelectFont.SetMarginBottom(12);
    buttonSelectFont.SetMarginStart(12);
    buttonSelectFont.SetMarginEnd(12);

    var gtkBox = Gtk.Box.New(Gtk.Orientation.Vertical, 0);
    gtkBox.Append(labelSelectedFont);
    gtkBox.Append(buttonSelectFont);

    var window = Gtk.ApplicationWindow.New((Gtk.Application) sender);
    window.Title = "Gtk4 Window";
    window.SetDefaultSize(300, 300);
    window.Child = gtkBox;

    buttonSelectFont.OnClicked += async (_, _) =>
    {
        try
        {
            var fontDialog = Gtk.FontDialog.New();
            var selectedFont = await fontDialog.ChooseFontAsync(window, null);
            labelSelectedFont.SetLabel(selectedFont?.ToString() ?? string.Empty);
        }
        catch (Exception ex)
        {
            //Prints "Dismissed by user" if dialog is cancelled
            Console.WriteLine(ex.Message);
        }
    };

    window.Show();
};
return application.RunWithSynchronizationContext(null);
