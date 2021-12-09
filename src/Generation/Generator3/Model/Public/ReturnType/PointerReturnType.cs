using Generator3.Converter;

namespace Generator3.Model.Public
{
    public class PointerReturnType : ReturnType
    {
        public override string NullableTypeName => TypeNameConverter.Pointer;

        protected internal PointerReturnType(GirModel.ReturnType model) : base(model)
        {
        }
    }
}
