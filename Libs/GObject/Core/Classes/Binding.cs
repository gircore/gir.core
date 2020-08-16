using System;

namespace GObject
{
    public class Binding : Object
    {
        internal new static Sys.Type GetGType() => new Sys.Type(Sys.Binding.get_type());

        #region Properties
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

        internal  Binding(IntPtr handle) : base(handle){ }

        public Binding(Object source, string sourceProperty, Object target, string targetProperty) 
            : base(
                Prop.With(propSource, source), 
                Prop.With(propSourceProperty, sourceProperty),
                Prop.With(propTarget, target),
                Prop.With(propTargetProperty, targetProperty),
                Prop.With(propFlags, Sys.BindingFlags.bidirectional))
        {
            
        }

        protected override void Dispose(bool disposing)
        {
            Sys.Binding.unbind(Handle);
            base.Dispose(disposing);
        }
    }
}