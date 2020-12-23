using System;
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
        protected static void SetTemplate(Type gtype, Bytes template)
        {
            IntPtr classPtr = TypeHelper.GetClassPointer(gtype);
            IntPtr bytesHandle = template.Handle;
            
            WidgetClass.Native.set_template(classPtr, bytesHandle);
        }
        
        protected void InitTemplate()
        {
            Native.init_template(Handle);
        }

        protected void BindTemplateChild<T>(string name, ref T field) where T : GObject.Object
        {
            var systemType = GetType();
            var gtype = TypeDictionary.Get(systemType);
            IntPtr ptr = Native.get_template_child(Handle, gtype.Value, name);
            field = WrapPointerAs<T>(ptr);
        }

        protected static void BindTemplateChild(Type gtype, string name)
        {
            IntPtr classPtr = TypeHelper.GetClassPointer(gtype);
            WidgetClass.Native.bind_template_child_full(classPtr, name, false, 0);
        }
        
        protected static void ConnectTemplateSignals(Type gtype, System.Type t)
        {
            var classPtr = TypeHelper.GetClassPointer(gtype);
            
            IntPtr ptr = Marshal.AllocHGlobal(Marshal.SizeOf(gtype));
            try
            {
                Marshal.StructureToPtr(gtype, ptr, true);
                //TODO Verify if OnConnectEvent and DestroyConnectData get garbage collected
                WidgetClass.Native.set_connect_func(classPtr, OnConnectEvent, ptr, DestroyConnectData);
            }
            finally
            {
                Marshal.FreeHGlobal(ptr);
            }
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

        private static void DestroyConnectData(IntPtr data)
        {
        }
    }
}
