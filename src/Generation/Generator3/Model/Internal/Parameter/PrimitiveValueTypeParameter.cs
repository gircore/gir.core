using Generator3.Converter;

namespace Generator3.Model.Internal
{
    public class PrimitiveValueTypeParameter : Parameter
    {
        public override string NullableTypeName => Model.AnyType.AsT0.GetName();

        public override string Direction => Model.GetDirection(
            @in: ParameterDirection.In,
            @out: ParameterDirection.Out,
            @outCallerAllocates: ParameterDirection.Ref,
            @inout: ParameterDirection.Ref
        );

        protected internal PrimitiveValueTypeParameter(GirModel.Parameter parameter) : base(parameter) { }
    }
}
