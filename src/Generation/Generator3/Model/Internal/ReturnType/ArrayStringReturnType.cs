namespace Generator3.Model.Internal
{
    public class ArrayStringReturnType : ReturnType
    {
        private GirModel.ArrayType ArrayType => Model.AnyType.AsT1;

        public bool IsMarshalAble => Model.Transfer != GirModel.Transfer.None || ArrayType.Length != null;

        public override string NullableTypeName
            => IsMarshalAble
                ? ArrayType.GetName()
                : TypeMapping.Pointer;

        protected internal ArrayStringReturnType(GirModel.ReturnType returnValue) : base(returnValue)
        {
            returnValue.AnyType.VerifyArrayType<GirModel.String>();
        }
    }
}
