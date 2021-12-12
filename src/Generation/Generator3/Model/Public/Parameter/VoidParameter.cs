using Generator3.Converter;

namespace Generator3.Model.Public
{
    public class VoidParameter : Parameter
    {
        public override string NullableTypeName => TypeNameConverter.Pointer;

        protected internal VoidParameter(GirModel.Parameter parameter) : base(parameter)
        {
            parameter.AnyType.VerifyType<GirModel.Void>();
        }
    }
}
