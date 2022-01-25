using Generator3.Converter;
using GirModel;

namespace Generator3.Model.Internal
{
    public class RecordReturnType : ReturnType
    {
        private GirModel.Record Type => (GirModel.Record) Model.AnyType.AsT0;

        public override string NullableTypeName => !IsPointer
            ? Type.GetFullyQualifiedInternalStructName()
            : Model.Transfer == Transfer.None
                ? Type.GetFullyQualifiedInternalUnownedHandle()
                : Type.GetFullyQualifiedInternalOwnedHandle();

        protected internal RecordReturnType(GirModel.ReturnType returnValue) : base(returnValue)
        {
            returnValue.AnyType.VerifyType<GirModel.Record>();
        }
    }
}
