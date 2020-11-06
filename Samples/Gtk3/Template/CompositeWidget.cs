using System;
using Gtk;

namespace GtkDemo
{
    
        //[TypeName(nameof(GtkCompositeWidget))]
        [Template("CompositeWidget.glade")]
        public class CompositeWidget : Bin
        {
            private static void BaseInit(IntPtr gClass)
            {
                Console.WriteLine("Composite BaseInit");
            }
            
            private static void ClassInit(IntPtr gClass, IntPtr classData)
            {
                Console.WriteLine("Class init Composite");
            }
            
            private static void InstanceInit(IntPtr gClass, IntPtr classData)
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
