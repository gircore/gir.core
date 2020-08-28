using System;

namespace GObject
{
    public class TypeModule : Object
    {
        private static readonly TypeDescriptor GTypeDescriptor = TypeDescriptor.For("GTypeModule", Sys.TypeModule.get_type);
        
        #region Constructors
        protected internal TypeModule(IntPtr ptr) : base(ptr) {}
        protected internal TypeModule(ConstructProp[] properties) : base(properties) {}
        #endregion Constructors
    }
}