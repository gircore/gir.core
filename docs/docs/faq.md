# Frequently asked questions
Common questions which can come up during development.

## DLL not found Exception

The `System.DllNotFoundException` can be thrown in the following cases:

- required DLLs or shared objects (the Unix equivalent of DLLs) are not installed in the operating system.
- `Module.Initialize()` was not called before instantiating an object in a namespace like `Gtk`, `Gdk` or `GtkSource`.
- required DLLs or shared objects are installed, but still `DllNotFoundException` is thrown. In this case, the names of the installed libraries probably don't match the names expected by `gir.core`. This can happen when [using a custom build binary](#how-can-i-use-gircore-with-a-custom-build-native-binary).

## How can I use gir.core with a custom build native binary?

The `gir.core` nuget packages are built against 3 different package sources:
1. Gnome SDK (Linux)
2. MSYS2 (Windows)
3. Homebrew (MacOS)

Each of those sources defines the names of the binaries which must be available to call into them. If a custom build binary is used, the resulting binary name may be different from the one specified by the package source, resulting in a `System.DllNotFoundException`.

In case of a custom build C binary it is recommended to use a custom `gir.core` build, too. Please follow the [build instructions](build.md) to get started. It is important to update the gir-files with the corresponding custom build gir-files.

This allows projects with custom build C binaries to create matching C# binaries without being dependent on one of the package sources.

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