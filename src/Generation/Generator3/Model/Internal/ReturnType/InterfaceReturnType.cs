using Generator3.Converter;

namespace Generator3.Model.Internal
{
    public class InterfaceReturnType : ReturnType
    {
        public override string NullableTypeName => Model.IsPointer
            ? TypeNameConverter.Pointer
            : Model.AnyType.AsT0.GetName();

        protected internal InterfaceReturnType(GirModel.ReturnType returnValue) : base(returnValue)
        {
            returnValue.AnyType.VerifyType<GirModel.Interface>();
        }
    }
}
