namespace Generator3.Model.Native
{
    public class RecordParameter : Parameter
    {
        //Native records are represented as SafeHandles and are not nullable
        public override string NullableTypeName => Model.Type.GetName();
        public override string Direction => Model switch
        {
            //Native records (SafeHandles) are not supporting ref
            { Direction: GirModel.Direction.InOut } => ParameterDirection.In,
            { Direction: GirModel.Direction.Out, CallerAllocates: true } => ParameterDirection.In,
            { Direction: GirModel.Direction.Out } => ParameterDirection.Out,
            _ => ParameterDirection.In
        };

        protected internal RecordParameter(GirModel.Parameter parameter) : base(parameter) { }
    }
}
