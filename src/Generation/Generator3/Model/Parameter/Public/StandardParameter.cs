namespace Generator3.Model.Public
{
    public class StandardParameter : Parameter
    {
        public override string NullableTypeName => Model.AnyType.Match(
            type => type.GetName() + GetDefaultNullable(),
            arrayType => arrayType.GetName() //TODO: Consider if StandardParameter should support arrays?
        );

        public override string Direction => Model.GetDirection(
            @in: ParameterDirection.In,
            @out: ParameterDirection.Out,
            @outCallerAllocates: ParameterDirection.Ref,
            @inout: ParameterDirection.Ref
        );

        protected internal StandardParameter(GirModel.Parameter parameter) : base(parameter) { }
    }
}
