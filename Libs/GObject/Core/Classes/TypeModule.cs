using System;

namespace GObject
{
    [Wrapper("GTypeModule")]
    public class TypeModule : Object
    {
        protected internal new static GObject.Type GetGType() => new GObject.Type(Sys.TypeModule.get_type());
        
        #region Constructors
        protected internal TypeModule(IntPtr ptr) : base(ptr) {}
        protected internal TypeModule(ConstructProp[] properties) : base(properties) {}
        #endregion Constructors
    }
}