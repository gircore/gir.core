using Generator3.Converter;

namespace Generator3.Model.Internal
{
    public class VoidParameter : Parameter
    {
        public override string NullableTypeName => TypeNameExtension.Pointer;

        protected internal VoidParameter(GirModel.Parameter parameter) : base(parameter)
        {
            parameter.AnyType.VerifyType<GirModel.Void>();
        }
    }
}
