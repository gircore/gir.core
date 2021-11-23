namespace Generator3.Model.Internal
{
    public class ClassInstanceParameter : InstanceParameter
    {
        public override string NullableTypeName => TypeMapping.Pointer;
        
        public ClassInstanceParameter(GirModel.InstanceParameter instanceParameter) : base(instanceParameter)
        {
        }
    }
}
