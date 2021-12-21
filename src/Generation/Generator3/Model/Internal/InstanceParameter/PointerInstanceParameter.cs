using Generator3.Converter;

namespace Generator3.Model.Internal
{
    public class PointerInstanceParameter : InstanceParameter
    {
        public override string NullableTypeName => TypeNameConverter.Pointer;

        public PointerInstanceParameter(GirModel.InstanceParameter instanceParameter) : base(instanceParameter)
        {
        }
    }
}
