using Generator3.Converter;

namespace Generator3.Model.Internal
{
    public class RecordParameter : Parameter
    {
        private GirModel.Record Type => (GirModel.Record) Model.AnyType.AsT0;

        //Native records are represented as SafeHandles and are not nullable
        public override string NullableTypeName => Type.GetFullyQualifiedInternalHandle();

        public override string Direction => Model.GetDirection(
            @in: ParameterDirection.In,
            @out: ParameterDirection.Out,
            @outCallerAllocates: ParameterDirection.In,
            @inout: ParameterDirection.In
        );

        protected internal RecordParameter(GirModel.Parameter parameter) : base(parameter)
        {
            parameter.AnyType.VerifyType<GirModel.Record>();
        }
    }
}
