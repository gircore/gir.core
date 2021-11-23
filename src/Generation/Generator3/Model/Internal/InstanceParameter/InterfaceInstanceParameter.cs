namespace Generator3.Model.Internal
{
    public class InterfaceInstanceParameter : InstanceParameter
    {
        public override string NullableTypeName => TypeMapping.Pointer;
        
        public InterfaceInstanceParameter(GirModel.InstanceParameter instanceParameter) : base(instanceParameter)
        {
        }
    }
}
