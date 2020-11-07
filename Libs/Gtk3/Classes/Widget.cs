using System;
using System.IO;
using Type = GObject.Type;

namespace Gtk
{
    public partial class Widget
    {
        #region Methods

        private static void ClassInit(Type gClass, System.Type type, IntPtr classData)
        {
            Console.WriteLine("Class init Widget");
        }

        private static void InitTemplate(Type gtype, System.Type type)
        {
            var template = GetTemplate(gtype, type);
           
            
            //WidgetClass.Native.set_template();

            /*SetTemplateFromStream(gtype, stream);
            BindTemplateChildren(gtype, type);
            (new SignalConnector(type)).ConnectSignals(gtype);
            */
        }

        private static GLib.Bytes? GetTemplate(Type gClass, System.Type type)
        {
            object[] attrs = type.GetCustomAttributes(typeof(TemplateAttribute), false);

            if (attrs.Length == 0)
                return null;

            var resourceName = ((TemplateAttribute) attrs[0]).Ui;
            Stream? stream = type.Assembly.GetManifestResourceStream(resourceName);

            if (stream == null)
                throw new Exception("Cannot get resource file '" + resourceName + "'");
            
            var size = (int)stream.Length;
            var buffer = new byte[size];
            stream.Read (buffer, 0, size);
            stream.Close ();

            return null; //new GLib.Bytes (buffer);
        }


        public void ShowAll() => Native.show_all(Handle);

        #endregion
    }
}
