using System;

namespace GObject
{
    public class Binding : Object
    {
        #region Properties

        private static readonly TypeDescriptor GTypeDescriptor = TypeDescriptor.For("GBinding", Sys.Binding.get_type);

        public static readonly Property<Object> SourceProperty = Property<Object>.Register<Binding>(
            "source",
            nameof(Source),
            (o) => o.Source,
            (o, v) => o.Source = v
        );

        public Object Source
        {
            get => GetProperty(SourceProperty);
            set => SetProperty(SourceProperty, value);
        }

        public static readonly Property<string> SourcePropertyProperty = Property<string>.Register<Binding>(
            "source-property",
            nameof(SourcePropertyName),
            (o) => o.SourcePropertyName,
            (o, v) => o.SourcePropertyName = v
        );

        public string SourcePropertyName
        {
            get => GetProperty(SourcePropertyProperty);
            set => SetProperty(SourcePropertyProperty, value);
        }

        public static readonly Property<Object> TargetProperty = Property<Object>.Register<Binding>(
            "target",
            nameof(Target),
            (o) => o.Target,
            (o, v) => o.Target = v
        );

        public Object Target
        {
            get => GetProperty(TargetProperty);
            set => SetProperty(TargetProperty, value);
        }

        public static readonly Property<string> TargetPropertyProperty = Property<string>.Register<Binding>(
            "target-property",
            nameof(TargetPropertyName),
            (o) => o.TargetPropertyName,
            (o, v) => o.TargetPropertyName = v
        );

        public string TargetPropertyName
        {
            get => GetProperty(TargetPropertyProperty);
            set => SetProperty(TargetPropertyProperty, value);
        }

        public static readonly Property<Sys.BindingFlags> FlagsProperty = Property<Sys.BindingFlags>.Register<Binding>(
            "flags",
            nameof(Flags),
            (o) => o.Flags,
            (o, v) => o.Flags = v
        );

        public Sys.BindingFlags Flags
        {
            get => GetProperty(FlagsProperty);
            set => SetProperty(FlagsProperty, value);
        }

        #endregion Properties

        internal Binding(IntPtr handle) : base(handle) { }

        public Binding(Object source, string sourceProperty, Object target, string targetProperty)
            : base(
                ConstructParam.With(SourceProperty, source),
                ConstructParam.With(SourcePropertyProperty, sourceProperty),
                ConstructParam.With(TargetProperty, target),
                ConstructParam.With(TargetPropertyProperty, targetProperty),
                ConstructParam.With(FlagsProperty, Sys.BindingFlags.bidirectional))
        { }

        protected override void Dispose(bool disposing)
        {
            Sys.Binding.unbind(Handle);
            base.Dispose(disposing);
        }
    }
}
