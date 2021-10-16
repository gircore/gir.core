namespace Generator3.Model.Native
{
    public class StandardParameter : Parameter
    {
        public override string NullableTypeName => Model.AnyType.Match(
            type => type.GetName() + GetDefaultNullable(),
            arrayType => arrayType.GetName() //TODO: Consider if StandardParameter should support arrays?
        );

        public override string Direction => Model switch
        {
            { Direction: GirModel.Direction.InOut } => ParameterDirection.Ref,
            { Direction: GirModel.Direction.Out, CallerAllocates: true } => ParameterDirection.Ref,
            { Direction: GirModel.Direction.Out } => ParameterDirection.Out,
            _ => ParameterDirection.In
        };

        protected internal StandardParameter(GirModel.Parameter parameter) : base(parameter) { }
    }
}
