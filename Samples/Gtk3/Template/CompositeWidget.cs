using System;
using System.Reflection;
using System.Runtime.InteropServices;
using GObject;
using Gtk;
using Type = GObject.Type;

namespace GtkDemo
{
    public class CompositeWidget : Bin, Orientable
    {
private static void RegisterInterfaces(Type gClass)
{
    var orientableInterfaceType = Gtk.Orientable.GTypeDescriptor.GType;

    var interfaceStruct = new InterfaceInfo(
        initFunc: InterfaceInit
    );
    
    IntPtr ptr = Marshal.AllocHGlobal(Marshal.SizeOf(interfaceStruct));
    Marshal.StructureToPtr(interfaceStruct, ptr, true);

    GObject.Global.Native.type_add_interface_static(gClass.Value, orientableInterfaceType.Value, ptr);
    Marshal.FreeHGlobal(ptr);
}
        
private static void InterfaceInit(IntPtr g_iface, IntPtr iface_data)
{
    
}

public Orientation Orientation { get; set; }

public void SetOrientation(Orientation orientation)
            => (this as Orientable).SetOrientation(orientation);
        
        private static void ClassInit(Type gClass, System.Type type, IntPtr classData)
        {
            SetTemplate(
                gtype: gClass, 
                template: Assembly.GetExecutingAssembly().ReadResource("CompositeWidget.ui")
            );
            BindTemplateChild(gClass, nameof(Button));
            BindTemplateSignals(gClass, type);
        }

        protected override void Initialize()
        {
            InitTemplate();
            ConnectTemplateChildToField(nameof(Button), ref Button);
        }

        private Button Button = default!;

        private void button_clicked(Button sender, System.EventArgs args)
        {
            sender.Label = "Clicked!";
        }
    }
}
