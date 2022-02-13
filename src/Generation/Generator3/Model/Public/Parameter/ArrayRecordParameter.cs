using Generator3.Converter;

namespace Generator3.Model.Public
{
    public class ArrayRecordParameter : Parameter
    {
        private GirModel.ArrayType ArrayType => Model.AnyType.AsT1;

        public override string NullableTypeName => ArrayType.Length is null
            ? TypeNameExtension.PointerArray
            : ((GirModel.Record) ArrayType.AnyType.AsT0).GetFullyQualified() + "[]";

        public override string Direction => Model.GetDirection(
            @in: ParameterDirection.In,
            @out: ParameterDirection.Out,
            @outCallerAllocates: ParameterDirection.Ref,
            @inout: ParameterDirection.Ref
        );

        protected internal ArrayRecordParameter(GirModel.Parameter parameter) : base(parameter)
        {
            parameter.AnyType.VerifyArrayType<GirModel.Record>();
        }
    }
}
