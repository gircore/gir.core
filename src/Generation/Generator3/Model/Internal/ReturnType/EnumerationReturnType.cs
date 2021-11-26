namespace Generator3.Model.Internal
{
    public class EnumerationReturnType : ReturnType
    {
        private GirModel.Enumeration Type => (GirModel.Enumeration) Model.AnyType.AsT0;

        //Internal does not define any bitfields. They are part of the Public API to avoid converting between them.
        public override string NullableTypeName => Type.Namespace.Name + "." + Type.GetName();

        protected internal EnumerationReturnType(GirModel.ReturnType returnValue) : base(returnValue)
        {
            returnValue.AnyType.VerifyType<GirModel.Enumeration>();   
        }
    }
}
