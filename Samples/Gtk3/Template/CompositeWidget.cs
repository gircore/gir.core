using System;
using Gtk;
using Type = GObject.Type;

namespace GtkDemo
{
    
        //[TypeName(nameof(GtkCompositeWidget))]
        [Template("CompositeWidget.glade")]
        public class CompositeWidget : Bin
        {
            private static void BaseInit(Type gClass, System.Type type)
            {
                Console.WriteLine("Composite BaseInit");
            }
            
            private static void ClassInit(Type gClass, System.Type type, IntPtr classData)
            {
                Console.WriteLine("Class init Composite");
                InitTemplate(gClass, type);
            }
            
            private static void InstanceInit(IntPtr instance, Type gClass, System.Type type)
            {
                Console.WriteLine("Instance init Composite");
            }
            
            [Connect] private Button Button = default!;

            public CompositeWidget()
            {
            
            }

            private void button_clicked(object obj, EventArgs args)
            {
                Button.Label = "Clicked!";
            } 
        }
}
