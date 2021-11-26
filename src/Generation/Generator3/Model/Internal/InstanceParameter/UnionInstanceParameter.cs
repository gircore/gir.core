namespace Generator3.Model.Internal
{
    public class UnionInstanceParameter : InstanceParameter
    {
        public override string NullableTypeName => TypeMapping.Pointer;
        
        public UnionInstanceParameter(GirModel.InstanceParameter instanceParameter) : base(instanceParameter)
        {
        }
    }
}
