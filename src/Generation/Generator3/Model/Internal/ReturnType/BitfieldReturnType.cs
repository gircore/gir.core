namespace Generator3.Model.Internal
{
    public class BitfieldReturnType : ReturnType
    {
        private GirModel.Bitfield Type => (GirModel.Bitfield) Model.AnyType.AsT0;

        public override string NullableTypeName => Type.GetFullyQualified();

        protected internal BitfieldReturnType(GirModel.ReturnType returnValue) : base(returnValue)
        {
            returnValue.AnyType.VerifyType<GirModel.Bitfield>();
        }
    }
}
