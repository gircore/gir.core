using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using GLib;
using GObject;
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

        #region Template without reflection
        protected static void SetTemplate2(Type gtype, Bytes template)
        {
            WidgetClass.Native.set_template(gtype.GetClassPointer(), GetHandle(template));
        }
        
        protected void InitTemplate2()
        {
            Native.init_template(Handle);
        }

        protected void BindTemplateChild2<T>(string name, ref T field) where T : GObject.Object
        {
            IntPtr ptr = Native.get_template_child(Handle, GetGType().Value, name);
            field = WrapPointerAs<T>(ptr);
        }

        protected static void BindTemplateChild2(Type gtype, string name)
        {
            WidgetClass.Native.bind_template_child_full(gtype.GetClassPointer(), name, false, 0);
        }

        protected static void OnConnectEvent(Type gtype, System.Type t)
        {
            IntPtr ptr = Marshal.AllocHGlobal(Marshal.SizeOf(gtype));
            Marshal.StructureToPtr(gtype, ptr, true);
            WidgetClass.Native.set_connect_func(gtype.GetClassPointer(), OnConnectEvent, ptr, DestroyConnectData);
        }

        private static void DestroyConnectData(IntPtr data)
        {
            //TODO clear connect data
        }
        
        private static void OnConnectEvent(IntPtr builder, IntPtr @object, string signal_name, string handler_name,
            IntPtr connect_object, ConnectFlags flags, IntPtr user_data)
        {
            if(!TryWrapPointerAs<Widget>(@object, out var eventSource))
                return;

            if(!TryWrapPointerAs<Widget>(connect_object, out var compositeWidget))
                return;

            MethodInfo? compositeWidgetCallbackMethodInfo = compositeWidget.GetType().GetMethod(
                name: handler_name, 
                bindingAttr: BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance
            );
            
            if (compositeWidgetCallbackMethodInfo is null)
                return;

            EventInfo? sourceEvent = eventSource.GetType().GetEvent(
                name: "On" + signal_name,
                bindingAttr: BindingFlags.Instance | BindingFlags.IgnoreCase | BindingFlags.Public
            );

            if (sourceEvent?.EventHandlerType is null)
                return;
            
            var del = Delegate.CreateDelegate(sourceEvent.EventHandlerType, compositeWidget, compositeWidgetCallbackMethodInfo);

            sourceEvent.AddMethod?.Invoke(eventSource, new object[] { del });
        }

        #endregion

        #region Template with reflection
        protected void InitTemplate()
        {
            Native.init_template(Handle);
            
            ForAllConnectAttributes(GetType(), (field, name) =>
            {
                IntPtr ptr = Native.get_template_child(Handle, GetGType().Value, name);
                field.SetValue (this, WrapPointer(ptr), TemplateFieldBindingFlags, null, null);
            });
        }
        
        protected static void SetTemplate(Type gtype, System.Type type)
        {
            Bytes? template = GetTemplate(type);
            if (template is null)
                return;

            WidgetClass.Native.set_template(gtype.GetClassPointer(), GetHandle(template));
            BindTemplateChildren(gtype, type);
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

        private static Bytes? GetTemplate(System.Type type)
        {
            object[] attrs = type.GetCustomAttributes(typeof(TemplateAttribute), false);

            if (attrs.Length == 0)
                return null;

            var resourceName = ((TemplateAttribute) attrs[0]).Ui;
            return type.Assembly.ReadResource(resourceName);
        }

        #endregion

        public void ShowAll() => Native.show_all(Handle);

        #endregion
    }
}
