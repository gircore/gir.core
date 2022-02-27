using Generator3.Converter;

namespace Generator3.Model.Internal
{
    public class ArrayClassReturnType : ReturnType
    {
        public override string NullableTypeName => TypeNameExtension.PointerArray;

        protected internal ArrayClassReturnType(GirModel.ReturnType returnValue) : base(returnValue)
        {
            returnValue.AnyType.VerifyArrayType<GirModel.Class>();
        }
    }
}
