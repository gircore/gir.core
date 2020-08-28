using System;

namespace GObject
{
    public class Binding : Object
    {
        private static readonly TypeDescriptor GTypeDescriptor = TypeDescriptor.For("GBinding", Sys.Binding.get_type);

        #region Properties

        public static readonly Property<Object> SourcePropertyDep = GObject.Property<Object>.Register<Binding>(
            "source",
            nameof(SourceEx),
            (o) => o.SourceEx,
            (o, v) => o.SourceEx = v
        );

        public Object SourceEx
        {
            get => GetProperty(SourcePropertyDep);
            set => SetProperty(SourcePropertyDep, value);
        }

        public static readonly Property<string> SourcePropertyPropertyDep = GObject.Property<string>.Register<Binding>(
            "source-property",
            nameof(SourcePropertyEx),
            (o) => o.SourcePropertyEx,
            (o, v) => o.SourcePropertyEx = v
        );

        public string SourcePropertyEx
        {
            get => GetProperty(SourcePropertyPropertyDep);
            set => SetProperty(SourcePropertyPropertyDep, value);
        }

        public static readonly Property<Object> TargetPropertyDep = GObject.Property<Object>.Register<Binding>(
            "target",
            nameof(TargetEx),
            (o) => o.TargetEx,
            (o, v) => o.TargetEx = v
        );

        public Object TargetEx
        {
            get => GetProperty(TargetPropertyDep);
            set => SetProperty(TargetPropertyDep, value);
        }

        public static readonly Property<string> TargetPropertyPropertyDep = GObject.Property<string>.Register<Binding>(
            "target-property",
            nameof(TargetPropertyEx),
            (o) => o.TargetPropertyEx,
            (o, v) => o.TargetPropertyEx = v
        );

        public string TargetPropertyEx
        {
            get => GetProperty(TargetPropertyPropertyDep);
            set => SetProperty(TargetPropertyPropertyDep, value);
        }

        public static readonly Property<Sys.BindingFlags> FlagsProperty = GObject.Property<Sys.BindingFlags>.Register<Binding>(
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

        private const string propSource = "source";
        public Object Source => GetObject<Object>(propSource);

        private const string propSourceProperty = "source-property";
        public string SourceProperty => GetStr(propSourceProperty);

        private const string propTarget = "target";
        public Object Target => GetObject<Object>(propTarget);

        private const string propTargetProperty = "target-property";
        public string TargetProperty => GetStr(propTargetProperty);

        private const string propFlags = "flags";
        #endregion Properties

        internal Binding(IntPtr handle) : base(handle) { }

        public Binding(Object source, string sourceProperty, Object target, string targetProperty)
            : base(
                ConstructProp.With(SourcePropertyDep, source),
                ConstructProp.With(SourcePropertyPropertyDep, sourceProperty),
                ConstructProp.With(TargetPropertyDep, target),
                ConstructProp.With(TargetPropertyPropertyDep, targetProperty),
                ConstructProp.With(FlagsProperty, Sys.BindingFlags.bidirectional))
        {

        }

        protected override void Dispose(bool disposing)
        {
            Sys.Binding.unbind(Handle);
            base.Dispose(disposing);
        }
    }
}