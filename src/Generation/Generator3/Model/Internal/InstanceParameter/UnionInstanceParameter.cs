using Generator3.Converter;

namespace Generator3.Model.Internal
{
    public class UnionInstanceParameter : InstanceParameter
    {
        public override string NullableTypeName => TypeNameConverter.Pointer;

        public UnionInstanceParameter(GirModel.InstanceParameter instanceParameter) : base(instanceParameter)
        {
        }
    }
}
