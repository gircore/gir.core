namespace Generator3.Generation.Code.Native
{
    public class ClassParameter : Parameter
    {
        //Native classes are represented as IntPtr and should not be nullable
        public override string NullableTypeName => _parameter.Type.GetName();
        public override string Direction => _parameter switch
        {
            { Direction: GirModel.Direction.InOut } => ParameterDirection.Ref,
            { Direction: GirModel.Direction.Out, CallerAllocates: true } => ParameterDirection.Ref,
            { Direction: GirModel.Direction.Out } => ParameterDirection.Out,
            _ => ParameterDirection.In
        };
        
        public ClassParameter(GirModel.Parameter parameter) : base(parameter) { }
    }
}
