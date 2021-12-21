using Generator3.Converter;

namespace Generator3.Model.Public
{
    public class ArrayClassParameter : Parameter
    {
        private GirModel.ArrayType ArrayType => Model.AnyType.AsT1;
        private GirModel.Class Class => (GirModel.Class) ArrayType.AnyType.AsT0;

        public override string NullableTypeName => Class.GetFullyQualified() + "[]";

        public override string Direction => Model.GetDirection(
            @in: ParameterDirection.In,
            @out: ParameterDirection.Out,
            @outCallerAllocates: ParameterDirection.Ref,
            @inout: ParameterDirection.Ref
        );

        protected internal ArrayClassParameter(GirModel.Parameter parameter) : base(parameter)
        {
            parameter.AnyType.VerifyArrayType<GirModel.Class>();
        }
    }
}
