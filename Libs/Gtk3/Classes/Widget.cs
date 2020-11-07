using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using GLib;
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
            Bytes? template = GetTemplate(type);
            if (template is null)
                return;

            WidgetClass.Native.set_template(gtype.GetClassPointer(), (IntPtr) template);
            BindTemplateChildren(gtype, type);
        }

        private static Bytes? GetTemplate(System.Type type)
        {
            object[] attrs = type.GetCustomAttributes(typeof(TemplateAttribute), false);

            if (attrs.Length == 0)
                return null;

            var resourceName = ((TemplateAttribute) attrs[0]).Ui;
            Stream? stream = type.Assembly.GetManifestResourceStream(resourceName);

            if (stream == null)
                throw new Exception("Cannot get resource file '" + resourceName + "'");

            var size = (int) stream.Length;
            var buffer = new byte[size];
            stream.Read(buffer, 0, size);
            stream.Close();

            return Bytes.From(buffer);
        }

        private static void BindTemplateChildren(Type gtype, System.Type t)
        {
            const BindingFlags flags = BindingFlags.Public
                                       | BindingFlags.NonPublic
                                       | BindingFlags.DeclaredOnly
                                       | BindingFlags.Instance;

            foreach (var field in t.GetFields(flags))
            {
                object[]? attrs = field.GetCustomAttributes(typeof(ConnectAttribute), true);

                if (attrs.Length == 0)
                    continue;

                var attr = (ConnectAttribute) attrs[0];
                var name = attr.WidgetName ?? field.Name;
                
                //TODO: fill internal child
                WidgetClass.Native.bind_template_child_full(gtype.GetClassPointer(), name, false, 0);
            }
        }
        
        public void ShowAll() => Native.show_all(Handle);

        #endregion
    }
}
