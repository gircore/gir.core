using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Gtk
{
    public partial class Builder
    {
        #region Constructors
        public Builder(string template) : this(Native.@new())
        {
            var templateContent = GetTemplate(Assembly.GetCallingAssembly(), template);
            var length = (ulong) Encoding.UTF8.GetByteCount(templateContent);
            Native.add_from_string(Handle, templateContent, length, out IntPtr error);
            HandleError(error);
        }
        #endregion
        
        #region Methods

        public IntPtr GetObject(string name) => Native.get_object(Handle, name);

        public void Connect(Widget connector)
        {
            ConnectFields(connector);
            ConnectSignals(connector);
        }

        private void ConnectSignals(object obj)
        {
            Native.connect_signals_full(Handle, OnConnectEvent, IntPtr.Zero);
        }

        private void OnConnectEvent(IntPtr builder, IntPtr @object, string signal_name, string handler_name, IntPtr connect_object, GObject.ConnectFlags flags, IntPtr user_data)
        {
            //TODO Errorhandling
            if(!GetObject<Widget>(@object, out var signalsender) || !GetObject<Widget>(connect_object, out var connector))
                return;

            MethodInfo? method = connector.GetType().GetMethod(handler_name, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            EventInfo? senderEvent = signalsender.GetType().GetEvent(signal_name, BindingFlags.Instance | BindingFlags.IgnoreCase | BindingFlags.Public);

            if (senderEvent is null || method is null)
                return;
            
            Type? eventType = senderEvent.EventHandlerType;

            if (eventType is null)
                return;

            var del = Delegate.CreateDelegate(eventType, connector, method);

            senderEvent.AddMethod?.Invoke(signalsender, new object[] { del });
        }

        private void ConnectFields(object obj)
        {
            var flags = BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.NonPublic | BindingFlags.Public;
            var fields = obj.GetType().GetFields(flags);

            foreach(var field in fields)
            {
                var attributes = field.GetCustomAttributes(typeof(ConnectAttribute), false);

                if(attributes.Length == 0)
                    continue;

                var connectAttribute = (ConnectAttribute) attributes[0];
                var element = connectAttribute.WidgetName ?? field.Name;

                if(!typeof(Widget).IsAssignableFrom(field.FieldType))
                    throw new Exception($"{field.FieldType.Name} must be a {nameof(Widget)}");

                var constructor = field.FieldType.GetConstructors(BindingFlags.NonPublic|BindingFlags.Public|BindingFlags.Instance)
                    .Where(CheckConstructor)
                    .FirstOrDefault();

                if(constructor is null)
                    throw new Exception($"{field.ReflectedType?.FullName} Field {field.Name}: Could not find a constructor with one parameter of {nameof(IntPtr)} to create a {field.FieldType.FullName}");

                var ptr = GetObject(element);

                if(ptr == IntPtr.Zero)
                    throw new Exception($"{field.ReflectedType?.FullName} Field {field.Name}: Could not find an element in the template with the name {element}");

                var newElement = constructor.Invoke(new object[] {ptr});
                field.SetValue(obj, newElement);
            }
        }

        private bool CheckConstructor(ConstructorInfo constructorInfo)
        {
            var parameters = constructorInfo.GetParameters();

            if(parameters.Length != 1)
                return false;

            if(parameters.First().ParameterType != typeof(IntPtr))
                return false;

            return true;
        }
        
        private static string GetTemplate(Assembly assembly, string template)
        {
            using Stream? stream = assembly.GetManifestResourceStream(template);

            if(stream is null)
                throw new Exception ($"Cannot get resource file '{template}'");

            return ReadFromStream(stream);
        }
        
        private static string ReadFromStream(Stream stream)
        {
            using var ms = new MemoryStream();
            stream.CopyTo(ms);

            byte[]? buffer = ms.ToArray();
            
            return Encoding.UTF8.GetString(buffer, 0, buffer.Length);
        }
        #endregion
    }
}
