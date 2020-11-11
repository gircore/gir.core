using System;
using System.Reflection;
using GObject;
using Gtk;
using Type = GObject.Type;

namespace GtkDemo
{
    public class CompositeWidget : Bin
    {
        private static void ClassInit(Type gClass, System.Type type, IntPtr classData)
        {
            SetTemplate2(
                gtype: gClass, 
                template: Assembly.GetExecutingAssembly().ReadResource("CompositeWidget.glade")
            );
            BindTemplateChild2(gClass, nameof(Button));
        }

        protected override void Initialize()
        {
            InitTemplate2();
            BindTemplateChild2(nameof(Button), ref Button);
        }

        private Button Button = default!;

        public CompositeWidget()
        {
        }

        private void button_clicked(object obj, EventArgs args)
        {
            Button.Label = "Clicked!";
        }
    }
}
