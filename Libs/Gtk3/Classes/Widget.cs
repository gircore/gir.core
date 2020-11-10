using System;
using System.IO;
using System.Reflection;
using GLib;
using BindingFlags = System.Reflection.BindingFlags;
using Type = GObject.Type;

namespace Gtk
{
    public partial class Widget
    {
        #region Field
        private const BindingFlags TemplateFieldBindingFlags = BindingFlags.Public
                                                               | BindingFlags.NonPublic
                                                               | BindingFlags.DeclaredOnly
                                                               | BindingFlags.Instance;
        #endregion
        #region Methods

        protected override void Initialize()
        {
            base.Initialize();
            System.Type myType = GetType();
            Type gtype = GetGType();

            Native.init_template(Handle);
            
            ForAllConnectAttributes(myType, (field, name) =>
            {
                IntPtr ptr = Native.get_template_child(Handle, gtype.Value, name);
                field.SetValue (this, WrapPointer(ptr), TemplateFieldBindingFlags, null, null);
            });
        }

        private static void ClassInit(Type gClass, System.Type type, IntPtr classData)
        {
            Console.WriteLine("Class init Widget");
            InitTemplate(gClass, type);
        }

        protected static void InitTemplate(Type gtype, System.Type type)
        {
            Bytes? template = GetTemplate(type);
            if (template is null)
                return;

            WidgetClass.Native.set_template(gtype.GetClassPointer(), GetHandle(template));
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

        private static void BindTemplateChildren(Type gtype, IReflect t)
        {
            ForAllConnectAttributes(t, (field, name) => 
                WidgetClass.Native.bind_template_child_full(gtype.GetClassPointer(), name, false, 0)
            );
        }
        
        private static void ForAllConnectAttributes(IReflect t, Action<FieldInfo, string> action)
        {
            foreach (var field in t.GetFields(TemplateFieldBindingFlags))
            {
                object[]? attrs = field.GetCustomAttributes(typeof(ConnectAttribute), true);

                if (attrs.Length == 0)
                    continue;

                var attr = (ConnectAttribute) attrs[0];
                var name = attr.WidgetName ?? field.Name;

                action(field, name);
            }
        }
        
        public void ShowAll() => Native.show_all(Handle);

        #endregion
    }
}
