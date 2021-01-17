using System;
using Gtk;
using Global = Gtk.Global;
using Object = GObject.Object;

namespace GtkDemo
{
    /// <summary>
    /// Minimalist demo program demonstrating the GTK templating system
    /// </summary>
    public static class Program
    {
        #region Methods
        
        public static void Main(string[] args)
        {
            Global.Init();
            var c = new CompositeWidget();

            /* In the generated code the orientable property needs to be defined like this:
             
             Some generated visiblity must probably be changed manually be adopted to be visible. I did not update the generator.
             
public static readonly Property<Orientation> OrientationProperty = Property<Orientation>.Wrap<Orientable>(
    Native.OrientationProperty,
    nameof(Orientation),
    (o) => o.Orientation,
    (o, v) => o.Orientation = v,
    EnumHelper.GetOrientationType,
    Types.Enum
);
             */
            
            c.Orientation = Orientation.Vertical; //Write Managed data
            Console.WriteLine(c.OrientationInterfaceAccess); //Read via C world
            c.OrientationInterfaceAccess = Orientation.Horizontal; //Write via C world
            Console.WriteLine(c.Orientation); //Read managed data
            
            var mainWindow = new Window("MyWindow")
            {
                DefaultWidth = 300, 
                DefaultHeight = 200, 
                Child = c,
                [Window.DestroySignal] = (o, e) => Global.MainQuit()
            };
            
            mainWindow.ShowAll();
            Global.Main();
        }
        #endregion
    }
}
