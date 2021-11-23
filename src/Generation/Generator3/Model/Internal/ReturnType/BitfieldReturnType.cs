namespace Generator3.Model.Internal
{
    public class BitfieldReturnType : ReturnType
    {
        private GirModel.Bitfield Type => (GirModel.Bitfield) Model.AnyType.AsT0;

        //Internal does not define any bitfields. They are part of the Public API to avoid converting between them.
        public override string NullableTypeName => Type.Namespace.Name + "." + Type.GetName();

        protected internal BitfieldReturnType(GirModel.ReturnType returnValue) : base(returnValue)
        {
            returnValue.AnyType.VerifyType<GirModel.Bitfield>();   
        }
    }
}
