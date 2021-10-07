namespace Generator3.Generation.Code.Native
{
    public class StandardParameter : Parameter
    {
        public override string NullableTypeName => _parameter.Type.GetName() + GetDefaultNullable();

        public override string Direction => _parameter switch
        {
            { Direction: GirModel.Direction.InOut } => ParameterDirection.Ref,
            { Direction: GirModel.Direction.Out, CallerAllocates: true } => ParameterDirection.Ref,
            { Direction: GirModel.Direction.Out } => ParameterDirection.Out,
            _ => ParameterDirection.In
        };
        
        protected internal StandardParameter(GirModel.Parameter parameter) : base(parameter) { }
    }
}
