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
            IntPtr bytesHandle = template.Handle;
            
            WidgetClass.Native.set_template(classPtr, bytesHandle);
        }
        
        protected void InitTemplate()
        {
            Native.init_template(Handle);
        }

        protected void ConnectTemplateChildToField<T>(string name, ref T field) where T : GObject.Object
        {
            var systemType = GetType();
            var gtype = TypeDictionary.Get(systemType);
            IntPtr ptr = Native.get_template_child(Handle, gtype.Value, name);
            field = WrapHandle<T>(ptr, false);
        }

        protected static void BindTemplateChild(Type gtype, string name)
        {
            IntPtr classPtr = TypeHelper.GetClassPointer(gtype);
            WidgetClass.Native.bind_template_child_full(classPtr, name, false, 0);
        }
        
        protected static void BindTemplateSignals(Type gtype, System.Type t)
        {
            var classPtr = TypeHelper.GetClassPointer(gtype);
            
            MarshalHelper.ToPtrAndFree(gtype, (ptr) => 
            {
                //TODO Verify if OnConnectEvent and DestroyConnectData get garbage collected
                WidgetClass.Native.set_connect_func(classPtr, OnConnectEvent, ptr, DestroyConnectData);
            });
        }
        
        private static void OnConnectEvent(IntPtr builder, IntPtr @object, string signal_name, string handler_name,
            IntPtr connect_object, ConnectFlags flags, IntPtr user_data)
        {
            if(!TryWrapHandle<Widget>(@object, false, out var eventSender))
                return;

            if (!TryGetEvent(eventSender.GetType(), signal_name, out EventInfo? @event))
                return;

            if (@event.EventHandlerType is null)
                return;
            
            if(!TryWrapHandle<Widget>(connect_object, false, out var compositeWidget))
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
