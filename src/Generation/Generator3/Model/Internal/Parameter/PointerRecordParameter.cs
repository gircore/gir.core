using Generator3.Converter;

namespace Generator3.Model.Internal
{
    public class PointerRecordParameter : Parameter
    {
        public override string NullableTypeName => TypeNameExtension.Pointer;

        protected internal PointerRecordParameter(GirModel.Parameter parameter) : base(parameter)
        {
            parameter.AnyType.VerifyType<GirModel.Record>();
        }
    }
}
