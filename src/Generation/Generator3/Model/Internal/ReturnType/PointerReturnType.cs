using Generator3.Converter;

namespace Generator3.Model.Internal
{
    public class PointerReturnType : ReturnType
    {
        public override string NullableTypeName => TypeNameConverter.Pointer;

        protected internal PointerReturnType(GirModel.ReturnType returnValue) : base(returnValue)
        {
            returnValue.AnyType.VerifyType<GirModel.Pointer>();
        }
    }
}
