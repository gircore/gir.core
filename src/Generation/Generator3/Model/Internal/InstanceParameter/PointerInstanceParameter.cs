namespace Generator3.Model.Internal
{
    public class PointerInstanceParameter : InstanceParameter
    {
        public override string NullableTypeName => TypeMapping.Pointer;
        
        public PointerInstanceParameter(GirModel.InstanceParameter instanceParameter) : base(instanceParameter)
        {
        }
    }
}
