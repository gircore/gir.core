using Generator3.Converter;

namespace Generator3.Model.Internal
{
    public class ClassParameter : Parameter
    {
        public override string NullableTypeName => TypeNameExtension.Pointer;

        public override string Direction => Model.GetDirection(
            @in: ParameterDirection.In,
            @out: ParameterDirection.Out,
            @outCallerAllocates: ParameterDirection.Ref,
            @inout: ParameterDirection.Ref
        );

        protected internal ClassParameter(GirModel.Parameter parameter) : base(parameter)
        {
            parameter.AnyType.VerifyType<GirModel.Class>();
        }
    }
}
