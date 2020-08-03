using System;

namespace GObject
{
    public class GConvertingProperty<T1, T2> : NamedProperty<T1>, Property<T1>
    {
        private readonly Func<string, T2> get;
        private readonly Action<T2, string> set;
        private readonly System.Converter<T1, T2> to;
        private readonly System.Converter<T2, T1> from;

        public T1 Value 
        {
            get => from(get(name));
            set => set(to(value), name); 
        }

        public GConvertingProperty(Object obj, string name, Func<string, T2> get, Action<T2, string> set, System.Converter<T1, T2> to, System.Converter<T2, T1> from) : base(obj, name)
        {
            this.get = get ?? throw new ArgumentNullException(nameof(get));
            this.set = set ?? throw new ArgumentNullException(nameof(set));
            this.to = to ?? throw new ArgumentNullException(nameof(to));
            this.from = from ?? throw new ArgumentNullException(nameof(from));
            obj.RegisterNotifyPropertyChangedEvent(name, () => base.OnChanged(Value));
        }
    }
}