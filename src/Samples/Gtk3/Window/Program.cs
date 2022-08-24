using Gtk;

Gtk.Module.Initialize();

var mainWindow = new Window("Hello Gtk!");
mainWindow.OnDestroy += (obj, args) => Functions.MainQuit();
mainWindow.ShowAll();

Functions.Main();
