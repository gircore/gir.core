using Generator3.Converter;

namespace Generator3.Model.Public
{
    public class RecordParameter : Parameter
    {
        private GirModel.Record Type => (GirModel.Record) Model.AnyType.AsT0;

        public override string NullableTypeName => Type.GetFullyQualified() + GetDefaultNullable();

        public override string Direction => Model.GetDirection(
            @in: ParameterDirection.In,
            @out: ParameterDirection.Out,
            @outCallerAllocates: ParameterDirection.Ref,
            @inout: ParameterDirection.Ref
        );

        protected internal RecordParameter(GirModel.Parameter parameter) : base(parameter)
        {
            parameter.AnyType.VerifyType<GirModel.Record>();
        }
    }
}
