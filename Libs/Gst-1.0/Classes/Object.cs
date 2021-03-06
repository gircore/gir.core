using GObject;

namespace Gst
{
    public partial class Object
    {
        public static readonly Property<string> NameProperty = Property<string>.Register<Object>(
            Native.NameProperty,
            nameof(Name),
            (o) => o.Name,
            (o, v) => o.Name = v
        );

        public string Name
        {
            get => GetProperty(NameProperty);
            set => SetProperty(NameProperty, value);
        }

        public void SetName(string name)
            => Native.Methods.SetName(Handle, name);

        // TODO: Marshal as enum
        public uint Flags
        {
            get
            {
                // Safe Version:
                Fields fields = GetObjectStruct<Fields>();
                return fields.flags;

                // Unsafe Version:
                // return ((Fields*) Handle)->flags;
            }
            set
            {
                // Safe Version:
                Fields fields = GetObjectStruct<Fields>();
                fields.flags = value;
                SetObjectStruct(fields);

                // Unsafe Version:
                // Fields* ptr = (Fields*) Handle;
                // ptr->flags = value;
            }
        }
    }
}
