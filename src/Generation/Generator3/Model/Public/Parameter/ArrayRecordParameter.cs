namespace Generator3.Model.Public
{
    public class ArrayRecordParameter : Parameter
    {
        private GirModel.ArrayType ArrayType => Model.AnyTypeReference.AnyType.AsT1;
        
        public override string NullableTypeName => ArrayType.Length is null
            ? TypeMapping.PointerArray
            : ((GirModel.Record) ArrayType.AnyTypeReference.AnyType.AsT0).GetFullyQualifiedPublicName() + "[]";

        public override string Direction => Model.GetDirection(
            @in: ParameterDirection.In,
            @out: ParameterDirection.Out,
            @outCallerAllocates: ParameterDirection.Ref,
            @inout: ParameterDirection.Ref
        );

        protected internal ArrayRecordParameter(GirModel.Parameter parameter) : base(parameter)
        {
            parameter.AnyTypeReference.AnyType.VerifyArrayType<GirModel.Record>();
        }
    }
}
