using Generator3.Converter;

namespace Generator3.Model.Internal
{
    public class UnionReturnType : ReturnType
    {
        private GirModel.Union Type => (GirModel.Union) Model.AnyType.AsT0;

        public override string NullableTypeName => Model.IsPointer
            ? TypeNameConverter.Pointer
            : Type.GetFullyQualifiedInternalStruct();

        protected internal UnionReturnType(GirModel.ReturnType returnValue) : base(returnValue)
        {
            returnValue.AnyType.VerifyType<GirModel.Union>();
        }
    }
}
