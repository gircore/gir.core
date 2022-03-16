using Generator3.Converter;

namespace Generator3.Model.Public
{
    public class InterfaceReturnType : ReturnType
    {
        private GirModel.Interface Type => (GirModel.Interface) Model.AnyType.AsT0;

        public override string NullableTypeName => Type.GetFullyQualified();

        protected internal InterfaceReturnType(GirModel.ReturnType returnValue) : base(returnValue)
        {
            returnValue.AnyType.VerifyType<GirModel.Interface>();
        }
    }
}
