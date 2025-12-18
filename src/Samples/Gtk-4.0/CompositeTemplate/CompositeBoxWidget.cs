using System;
using System.Runtime.InteropServices;

namespace CompositeTemplate;

[GObject.Subclass<Gtk.Box>]
//[Template("file.ui")]
public unsafe partial class CompositeBoxWidget
{
    static partial void CompositeTemplateClassInit(IntPtr cls, IntPtr clsdata)
    {
        var data = Ressource.FromAssembly("CompositeDialogWidget.ui");
        var classHandle = new Gtk.Internal.WidgetClassUnownedHandle(cls);
        Gtk.Internal.WidgetClass.SetTemplate(
            widgetClass: classHandle,
            templateBytes: data.Handle
        );

        /*Gtk.Internal.WidgetClass.BindTemplateChildFull(
            widgetClass: classHandle,
            name: GLib.Internal.NonNullableUtf8StringOwnedHandle.Create("Button"),
            internalChild: false,
            structOffset: 0
        );

        Gtk.Internal.WidgetClass.BindTemplateCallbackFull(
            widgetClass: classHandle,
            callbackName: GLib.Internal.NonNullableUtf8StringOwnedHandle.Create("Button"),
            callbackSymbol: null! //TODO
        );*/
    }

    static partial void CompositeTemplateDispose(IntPtr instance)
    {
        Gtk.Internal.Widget.DisposeTemplate(instance, GType);
    }
    
    static partial void CompositeTemplateInstanceInit(IntPtr instance, IntPtr cls)
    {
        Gtk.Internal.Widget.InitTemplate(instance);
    }
}
