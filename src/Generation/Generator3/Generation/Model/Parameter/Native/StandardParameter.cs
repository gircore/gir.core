namespace Generator3.Generation.Model.Native
{
    public class StandardParameter : Parameter
    {
        public override string NullableTypeName => Model.Type.GetName() + GetDefaultNullable();

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
