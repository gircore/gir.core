namespace Generator3.Generation.Code.Native
{
    public class RecordParameter : Parameter
    {
        //Native records are represented as SafeHandles and are not nullable
        public override string NullableTypeName => _parameter.Type.GetName();
        public override string Direction => _parameter switch
        {
            //Native records (SafeHandles) are not supporting ref
            { Direction: GirModel.Direction.InOut } => ParameterDirection.In,
            { Direction: GirModel.Direction.Out, CallerAllocates: true } => ParameterDirection.In,
            { Direction: GirModel.Direction.Out } => ParameterDirection.Out,
            _ => ParameterDirection.In
        };
        
        public RecordParameter(GirModel.Parameter parameter) : base(parameter) { }
    }
}
