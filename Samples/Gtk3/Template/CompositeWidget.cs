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
        public Orientation Orientation { get; set; }

        //Just for Demo purpose
        public Orientation OrientationInterfaceAccess
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
                var myClass = (ObjectClass*) gclass;

                var setProp = Marshal.GetFunctionPointerForDelegate<PropAccess>(MySetProperty);
                var getProp = Marshal.GetFunctionPointerForDelegate<PropAccess>(MyGetProperty);
                myClass->set_property = setProp;
                myClass->get_property = getProp;
                
                InstallProperty(
                    id: 1,
                    objectClass: gclass,
                    property: Orientable.OrientationProperty
                );
            }
        }

        private delegate void PropAccess(IntPtr handle, uint propertyId, ref Value value, IntPtr paramSpec);

        private static void MySetProperty(IntPtr handle, uint propertyId, ref Value value, IntPtr paramSpec)
        {
            var obj = WrapHandle<CompositeWidget>(handle);

            if (propertyId == 1)
                obj.Orientation = value.Extract<Orientation>();
        }
        
        private static void MyGetProperty(IntPtr handle, uint propertyId, ref Value value, IntPtr paramSpec)
        {
            var obj = WrapHandle<CompositeWidget>(handle);

            if (propertyId == 1)
                value.SetEnum(obj.Orientation);
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
