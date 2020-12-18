using System;
using System.Diagnostics.CodeAnalysis;
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
            IntPtr bytesHandle = GetHandle(template);
            
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
            if(!TryWrapPointerAs<Widget>(@object, out var eventSender))
                return;

            if (!TryGetEvent(eventSender.GetType(), signal_name, out EventInfo? @event))
                return;

            if (@event.EventHandlerType is null)
                return;
            
            if(!TryWrapPointerAs<Widget>(connect_object, out var compositeWidget))
                return;

            if (!TryGetMethod(compositeWidget.GetType(), handler_name, out MethodInfo? compositeWidgetEventHandler))
                return;

            var eventHandlerDelegate = Delegate.CreateDelegate(@event.EventHandlerType, compositeWidget, compositeWidgetEventHandler);

            ConnectEventWithDelegate(@event, eventSender, eventHandlerDelegate);
        }

        private static bool TryGetEvent(System.Type type, string eventName, [NotNullWhen(true)] out EventInfo? eventInfo)
        {
            eventInfo = type.GetEvent(
                name: "On" + eventName,
                bindingAttr: BindingFlags.Instance | BindingFlags.IgnoreCase | BindingFlags.Public
            );

            return eventInfo is not null;
        }

        private static bool TryGetMethod(System.Type compositeWidgetType, string methodName, [NotNullWhen(true)] out MethodInfo? methodInfo)
        {
            methodInfo = compositeWidgetType.GetMethod(
                name: methodName, 
                bindingAttr: BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance
            );

            return methodInfo is not null;
        }

        private static void ConnectEventWithDelegate(EventInfo eventInfo, object eventObject, Delegate del)
        {
            eventInfo.AddMethod?.Invoke(eventObject, new object[] { del });
        }

        private static void DestroyConnectData(IntPtr data)
        {
        }
    }
}
