using Generator3.Converter;
using GirModel;

namespace Generator3.Model.Internal
{
    public class RecordReturnTypeForCallback : ReturnType
    {
        private GirModel.Record Type => (GirModel.Record) Model.AnyType.AsT0;

        public override string NullableTypeName => !IsPointer
            ? Type.GetFullyQualifiedInternalStructName()
            : Type.GetFullyQualifiedInternalHandle();

        protected internal RecordReturnTypeForCallback(GirModel.ReturnType returnValue) : base(returnValue)
        {
            returnValue.AnyType.VerifyType<GirModel.Record>();
        }
    }
}
