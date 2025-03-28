using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using GObject.Internal;
using Gtk.Internal;

namespace Gtk;

public partial class Builder
{
    #region Constructors
    private Builder(string templateXml, bool owned) : this(new BuilderHandle(Internal.Builder.NewFromString(GLib.Internal.NonNullableUtf8StringOwnedHandle.Create(templateXml), Encoding.UTF8.GetByteCount(templateXml)), owned))
    {
    }
    public Builder(string embeddedTemplateName) : this(GetTemplate(Assembly.GetCallingAssembly(), embeddedTemplateName), true)
    {
    }
    #endregion

    #region Methods

    public void Connect(Widget connector)
    {
        ConnectFields(connector);
        //ConnectSignals(connector);
    }

    /*
     private void ConnectSignals(object obj)
    {
        //Native.connect_signals_full(Handle, OnConnectEvent, IntPtr.Zero);
    }
    */

    public IntPtr GetPointer(string name)
    {
        return Internal.Builder.GetObject(this.Handle.DangerousGetHandle(), GLib.Internal.NonNullableUtf8StringOwnedHandle.Create(name));
    }

    /*
    private void OnConnectEvent(IntPtr builder, IntPtr @object, string signal_name, string handler_name, IntPtr connect_object, GObject.ConnectFlags flags, IntPtr user_data)
    {
        if (!TryWrapHandle(@object, false, out Widget? signalsender) || !TryWrapHandle(connect_object, false, out Widget? connector))
            return;

        var eventFlags = BindingFlags.Instance | BindingFlags.IgnoreCase | BindingFlags.Public;

        MethodInfo? method = connector.GetType().GetMethod(handler_name, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

        if (method is null)
            throw new Exception($"Could not find instance method with name: {handler_name}");

        EventInfo? senderEvent = signalsender.GetType().GetEvent(signal_name, eventFlags);

        if (senderEvent is null)
            throw new Exception($"Could not find event with name: {signal_name}");

        Type? eventType = senderEvent.EventHandlerType;

        if (eventType is null)
            throw new Exception($"Event {signal_name} has no {nameof(senderEvent.EventHandlerType)}");

        var del = Delegate.CreateDelegate(eventType, connector, method);

        MethodInfo? addMethod = senderEvent.AddMethod;
        if (addMethod is null)
            throw new Exception($"Event {signal_name} has no {senderEvent.AddMethod}");

        addMethod.Invoke(signalsender, new object[] { del });
    }*/

    private void ConnectFields(object obj)
    {
        const BindingFlags Flags = BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.NonPublic | BindingFlags.Public;
        FieldInfo[] fields = obj.GetType().GetFields(Flags);

        foreach (FieldInfo field in fields)
        {
            var attributes = field.GetCustomAttributes(typeof(ConnectAttribute), false);

            if (attributes.Length == 0)
                continue;

            var connectAttribute = (ConnectAttribute) attributes[0];
            var element = connectAttribute.WidgetName ?? field.Name;

            if (!typeof(Widget).IsAssignableFrom(field.FieldType))
                throw new Exception($"{field.FieldType.Name} must be a {nameof(Widget)}");

            var ptr = GetPointer(element);

            if (ptr == IntPtr.Zero)
                throw new Exception($"{field.ReflectedType?.FullName} Field {field.Name}: Could not find an element in the template with the name {element}");

            var newElement = InstanceWrapper.WrapHandle<Widget>(ptr, false);
            field.SetValue(obj, newElement);
        }
    }

    private static string GetTemplate(Assembly assembly, string template)
    {
        using Stream? stream = assembly.GetManifestResourceStream(template);

        if (stream is null)
            throw new Exception($"Cannot get resource file '{template}'");

        return ReadFromStream(stream);
    }

    private static string ReadFromStream(Stream stream)
    {
        using var ms = new MemoryStream();
        stream.CopyTo(ms);

        var buffer = ms.ToArray();

        return Encoding.UTF8.GetString(buffer, 0, buffer.Length);
    }
    #endregion
}
