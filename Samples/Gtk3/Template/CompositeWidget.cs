using System;
using System.Reflection;
using System.Runtime.InteropServices;
using GObject;
using Gtk;
using Type = GObject.Type;

namespace GtkDemo
{
    public static class OrientationHelper
    {
        [DllImport("libgtk-3.so.0", EntryPoint = "gtk_orientation_get_type")]
        private static extern ulong gtk_orientation_get_type();

        public static Type GetGType()
            => new Type(gtk_orientation_get_type());
    }
    
    public class CompositeWidget : Bin, Orientable
    {
        public Orientation Orientation
        {
            get => GetProperty(Orientable.OrientationProperty);
            set => SetProperty(Orientable.OrientationProperty, value);
        }

        private static void ClassInit(Type gClass, System.Type type, IntPtr gclass, IntPtr classData)
        {
            SetTemplate(
                gtype: gClass, 
                template: Assembly.GetExecutingAssembly().ReadResource("CompositeWidget.ui")
            );
            BindTemplateChild(gClass, nameof(Button));
            BindTemplateSignals(gClass, type);

            unsafe
            {
                var myClass = (BinClass*) gclass;

                var setProp = Marshal.GetFunctionPointerForDelegate<PropAccess>(MySetProperty);
                var getProp = Marshal.GetFunctionPointerForDelegate<PropAccess>(MyGetProperty);
                myClass->parent_class.parent_class.parent_class.set_property = setProp;
                myClass->parent_class.parent_class.parent_class.get_property = getProp;
                
                InstallPropertyEnum(
                    objectClass: (IntPtr) myClass,
                    id: 1, 
                    name: "orientation", 
                    nick: "Orientation", 
                    blurb: "Orientation prop",
                    enumType: OrientationHelper.GetGType(),
                    defaultValue: 0, 
                    flags: ParamFlags.Readwrite
                );
            }
        }

        private delegate void PropAccess(IntPtr handle, uint propertyId, ref Value value, IntPtr paramSpec);

        private static void MySetProperty(IntPtr handle, uint propertyId, ref Value value, IntPtr paramSpec)
        {
            
        }
        
        private static void MyGetProperty(IntPtr handle, uint propertyId, ref Value value, IntPtr paramSpec)
        {
            
        }


        private static void RegisterInterfaces(Type gClass)
        {
            RegisterInterface(gClass, Gtk.Orientable.GTypeDescriptor.GType);
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
