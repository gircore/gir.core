using Generator3.Converter;

namespace Generator3.Model.Internal
{
    public class PrimitiveValueReturnType : ReturnType
    {
        public override string NullableTypeName => IsPointer
            ? TypeNameConverter.Pointer
            : Model.AnyType.AsT0.GetName();

        protected internal PrimitiveValueReturnType(GirModel.ReturnType returnValue) : base(returnValue)
        {
            returnValue.AnyType.VerifyType<GirModel.PrimitiveValueType>();
        }
    }
}
