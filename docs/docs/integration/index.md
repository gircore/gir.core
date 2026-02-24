# Integration packages

The GirCore project provides integration nuget packages which can generate boilerplate code to allow easy integration of dotnet with the GObject library stack.

The following nuget packages are available:
- [GObject-2.0.Integration](https://www.nuget.org/packages/GirCore.GObject-2.0.Integration): Makes it easy to create a subclass of a GObject. See the [FAQ](../faq.md#how-to-create-subclasses-of-a-gobject-based-class) for a sample.
- [Gtk-4.0.Integration](https://www.nuget.org/packages/GirCore.Gtk-4.0.Integration): Makes it easy to create a composite template class of a GTK widget. See the [samples](https://github.com/gircore/gir.core/blob/main/src/Samples/Gtk-4.0/CompositeTemplate/CompositeBoxWidget.cs) on GitHub.

## Diagnostic messages
Diagnostic messages are raised by the integration packages to assist with the usage of the available attributes

### GObject-2.0.Integration diagnostics
- [GirCore1001](diagnostic/1001.md) (Obsolete)
- [GirCore1002](diagnostic/1002.md)
- [GirCore1003](diagnostic/1003.md) (Obsolete)
- [GirCore1004](diagnostic/1004.md)
- [GirCore1005](diagnostic/1005.md)
- [GirCore1006](diagnostic/1006.md)
- [GirCore1007](diagnostic/1007.md)

### Gtk-4.0.Integration diagnostics
- [GirCore2001](diagnostic/2001.md)
- [GirCore2002](diagnostic/2002.md)
- [GirCore2003](diagnostic/2003.md)