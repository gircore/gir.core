namespace Generator3.Model.Native
{
    public class ClassParameter : Parameter
    {
        //Native classes are represented as IntPtr and should not be nullable
        public override string NullableTypeName => Model.Type.GetName();
        public override string Direction => Model switch
        {
            { Direction: GirModel.Direction.InOut } => ParameterDirection.Ref,
            { Direction: GirModel.Direction.Out, CallerAllocates: true } => ParameterDirection.Ref,
            { Direction: GirModel.Direction.Out } => ParameterDirection.Out,
            _ => ParameterDirection.In
        };

        protected internal ClassParameter(GirModel.Parameter parameter) : base(parameter) { }
    }
}
