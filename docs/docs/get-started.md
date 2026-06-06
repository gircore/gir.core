# Get started

To use the bindings create a C# project and just add the corresponding [nuget packages](https://www.nuget.org/profiles/GirCore).

There are a lot of [sample projects](https://github.com/gircore/gir.core/tree/main/src/Samples) available to get you started. Extensive [API documentation](https://gircore.github.io/api/index.html) is provided and the [original documentation](https://developer.gnome.org/documentation/introduction/overview/libraries.html) can also be referenced.

To run your app you can execute it with the classic command `dotnet run` inside your project folder.

## Required binaries
The gir.core project is *not* providing the actual C libraries but only the C# bindings. Please ensure that the corresponding packages are installed on your system otherwise the binding will not find a target to bind to during runtime.

In case the packages are not installed on your system please refer to the documentation of your systems package manager.

# [Linux](#tab/linux)
In most distributions the needed packages should already be installed. In case something is missing use the package manager of your distribution to install the missing dependencies.

# [Windows](#tab/windows)
The easiest way to get started on Windows is by installing the packages through msys2. The [MSYS2 Package Database](https://packages.msys2.org/) can be searched for matching packages.

1. Download msys2 from the [official website](https://www.msys2.org/).
2. Run `pacman -Syu` to update the package database.
3. Run `pacman -S mingw-w64-x86_64-XXX` to install a package named XXX.
4. Add the directory `C:/msys64/mingw64/bin` to the front of the PATH.

# [MacOS](#tab/mac)
Use the [Homebrew package database](https://formulae.brew.sh/) to find and install any needed packages.
***

### DLL not found Exception
The `System.DllNotFoundException` can be thrown in the following cases:

- required DLLs or shared objects (the Unix equivalent of DLLs) are not installed in the operating system.
- `Module.Initialize()` was not called before instantiating an object in a namespace like `Gtk`, `Gdk` or `GtkSource`.
- required DLLs or shared objects are installed, but still `DllNotFoundException` is thrown. In this case, the names of the installed libraries probably don't match the names expected by `gir.core`. This can happen when [using a custom build binary](custom-library.md).

## Property changed notifications
C# developers are familar with the `INotifyPropertyChanged` interface, which can be used to notify an event listener about a changed property. The GObject type system uses a different but similar approach.

Every class which inherits from `GObject.Object` has an event called `Object.OnNotify`. Subscriber to this event get notified about every changed property similar to `INotifyPropertyChanged`. The `NotifySignalArgs` event argument contains a `ParamSpec` instance which can be queried for the *native* property name via `GetName()`.

As the *native* `Object` instance is represented in C# the properties in C# are named differently (mostly camel cased) in comparision to their *native* counterparts. To be able to match the *native* property name with their managed one, every *native* property has a static `Property` descriptor which provides the managed and unmanged name. Additionally, it is possible to get or set the properties via their descriptor.

In Addition to the `OnNotify` event there is a static field for each event which describes the event. Similar to properties it provides the managed and unmanaged name of an event and allows to connect to it.

The GObject type system is more advanced than `INotifyPropertyChanged` as it allows to subscribe to specific properties of an instance: This means there are only events received for properties the application is actually interested in. This feature can be used if an event listener is registered via the `NotifySignal.Connect()` method instead of the `OnNotify` event and supplies a *detail* parameter containing the *native* name of the property to watch. (Other events have a *detail* parameter, too with different meanings.)

Sample Code:
```csharp
NotifySignal.Connect(
    sender: myObj,
    signalHandler: OnMyPropertyChanged,
    detail: "my-property"
);
```

Please remember that the detail information must contain the *native name* of a property which is available through the static property descriptor.

There is an alternative API available which avoids defining the name of the property. Every property definition has a `Notify()` method:

```csharp
Gtk.Button.LabelPropertyDefinition.Notify(
    sender: myButton,
    signalHandler: OnButtonLabelChanged
);
```

## GObject subclasses
Creating a subclass for a GObject based class requires creating a partial class with no parent class defined. An attribute is added which defines the parent class. Using the attribute results in a source generator generating the needed code to integrate the custom class with the GObject type system. The source generator is included in the [GObject-2.0 nuget package](https://www.nuget.org/packages/GirCore.GObject-2.0/) and thus automatically available to every project consuming a GirCore based library.

> [!NOTE]
> The integration with the native C world requires the bindings to create *unsafe* code. Please include `<AllowUnsafeBlocks>true</AllowUnsafeBlocks>` in your project file.

> [!TIP]
> To use custom subclasses indirectly (e.g. in UI files) they need to be registered first. This can be done at the very beginning of your program via `GirCore.Integration.Initialize`. Please ensure that all depending modules are initialized first (e.g. call `Gtk.Module.Initialize`).

```csharp
[GObject.Subclass<GObject.Object>]
public partial class MyObject
{
    partial void Initialize()
    {
        ...
    }
}
```

The `partial void Initialize()` method replaces a parameterless constructor and can be used to initialize the instance.

> [!TIP]
> The `Initialize` method is called always if an instance of the subclass is created. It does not matter if dotnet or GObject is creating the instance. This comes in handy for custom UI widgets which are created by GTK if a [composite template](#gtk-composite-templates) is used which contains a custom widget.

### Parameterized instance
Be aware that GirCore GObject integration requires instance creation via factory methods. Using regular C# constructors is not supported as it blocks deep integration with GObject itself. If a subclass is created the source generator automatically creates a factory function called `NewWithProperties` which allows to set native properties. It can be used to create a parametrized subclass.

> [!TIP]
> The usage of factory methods makes dependency injection harder. Depending on the application architecture and size it should be considered to either:
> 1. Create explicit factory classes or register a function directly in your dependency injection system.
> 2. Hide GirCore based classes and access them via interfaces to get a clean separation of concern.

```csharp
[GObject.Subclass<GObject.Object>]
public partial class MyObject
{
    private string? data;
 
    public static MyObject NewWithString(string data)
    {
        var obj = NewWithProperties([]);
        obj.data = data;

        return obj;
    }
}
```

### Subclassing a subclass
Subclassing a subclass is straight forward:

```csharp
[GObject.Subclass<MyObject>]
public partial class SubSubclass
{ 
    public static SubSubclass NewWithString()
    {
        return NewWithProperties([]);
    }
}

[GObject.Subclass<GObject.Object>]
public partial class MyObject
{ 
    public static MyObject NewWithString()
    {
        return NewWithProperties([]);
    }
}
```


## GTK composite templates
GTK composite templates allow creating a subclass from a `Gtk.Widget` and associate it with a `*.ui` file. In this way it is possible to creat custom widgets. There is a [sample](https://github.com/gircore/gir.core/tree/main/src/Samples/Gtk-4.0/CompositeTemplate) available demonstrating the different template loaders.

[!code-csharp[](../../src/Samples/Gtk-4.0/CompositeTemplate/CompositeBoxWidget.cs)]

The `GObject.Subclass` attribute is needed to define the base type of your custom widget. Additionally the `Gtk.Template` attribute is needed to define which UI file the widget should use. The generic attribute defines which template loader should be used. There are two `TemplateLoader` classes available:
* `Gtk.AssemblyResource`: Loads the UI file from an assembly resource
* `Gtk.GResource`: Loads the UI file from a registered GResource.

> [!TIP]
> You can implement the `Gtk.TemplateLoader` interface to create custom template loaders. This allows to retrieve your UI definition file from any location.

The `Gtk.Connect` attribute is used to connect a member to a certain member of the custom widgets UI file. If no name is specified the name of the member is used.