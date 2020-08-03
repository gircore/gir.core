using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using GObject;

namespace Gtk
{
    internal partial class Builder : GObject.Object
    {
        public Property<string> TranslationDomain { get; }
        
        internal Builder(string template, Assembly assembly) : this(Sys.Builder.@new())
        {
            using var stream = assembly.GetManifestResourceStream(template);

            if(stream is null)
                throw new Exception ($"Cannot get resource file '{template}'");

            var templateContent =  ReadFromStream(stream);

            AddFromString(templateContent);
        }
        internal Builder(string template) : this(template, Assembly.GetCallingAssembly()) { }

        internal Builder(IntPtr handle) : base(handle)
        {
            TranslationDomain = PropertyOfString("translation-domain");
        }

        private uint AddFromString(string template)
        {
            var result = Sys.Builder.add_from_string(this, template, (ulong) Encoding.UTF8.GetByteCount(template), out var error);
            HandleError(error);
            return result;
        }

        private static string ReadFromStream(Stream stream)
        {
            using var ms = new MemoryStream();
            stream.CopyTo(ms);

            var buffer = ms.ToArray();
            
            return Encoding.UTF8.GetString(buffer, 0, buffer.Length);
        }

        public IntPtr GetObject(string name) => Sys.Builder.get_object(this, name);

        public void Connect(Widget connector)
        {
            ConnectFields(connector);
            ConnectSignals(connector);
        }

        private void ConnectSignals(object obj)
        {
            Sys.Builder.connect_signals_full(this, OnConnectEvent, IntPtr.Zero);
        }

        private void OnConnectEvent(IntPtr builder, IntPtr @object, string signal_name, string handler_name, IntPtr connect_object, GObject.Sys.ConnectFlags flags, IntPtr user_data)
        {
            //TODO Errorhandling
            if(!TryGetObject(@object, out Widget signalsender) || !TryGetObject(connect_object, out Widget connector))
                return;

            const BindingFlags eventFlags = BindingFlags.Instance | BindingFlags.IgnoreCase | BindingFlags.Public;

            var method = connector.GetType().GetMethod(handler_name, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

            var senderEvent = signalsender.GetType().GetEvent(signal_name, eventFlags);
            var eventType = senderEvent.EventHandlerType;

            var del = Delegate.CreateDelegate(eventType, connector, method);

            senderEvent.AddMethod.Invoke(signalsender, new object[] { del });
        }

        private void ConnectFields(object obj)
        {
            const BindingFlags flags = BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.NonPublic | BindingFlags.Public;
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
                    throw new Exception($"{field.ReflectedType.FullName} Field {field.Name}: Could not find a constructor with one parameter of {nameof(IntPtr)} to create a {field.FieldType.FullName}");

                var ptr = GetObject(element);

                if(ptr == IntPtr.Zero)
                    throw new Exception($"{field.ReflectedType.FullName} Field {field.Name}: Could not find an element in the template with the name {element}");

                var newElement = constructor.Invoke(new object[] {ptr});
                field.SetValue(obj, newElement);
            }
        }

        private static bool CheckConstructor(ConstructorInfo constructorInfo)
        {
            var parameters = constructorInfo.GetParameters();

            if(parameters.Length != 1)
                return false;

            return parameters.First().ParameterType == typeof(IntPtr);
        }
    }
}