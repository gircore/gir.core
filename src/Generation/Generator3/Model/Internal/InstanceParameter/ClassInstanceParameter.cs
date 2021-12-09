using Generator3.Converter;

namespace Generator3.Model.Internal
{
    public class ClassInstanceParameter : InstanceParameter
    {
        public override string NullableTypeName => TypeNameConverter.Pointer;
        
        public ClassInstanceParameter(GirModel.InstanceParameter instanceParameter) : base(instanceParameter)
        {
        }
    }
}
