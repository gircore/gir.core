using Generator3.Converter;

namespace Generator3.Model.Internal
{
    public class ArrayRecordParameter : Parameter
    {
        private GirModel.ArrayType ArrayType => Model.AnyType.AsT1;

        public override string NullableTypeName => ArrayType.Length is null
            ? TypeNameConverter.PointerArray
            : ((GirModel.Record) ArrayType.AnyType.AsT0).GetFullyQualifiedInternalStructName() + "[]";

        public override string Attribute => ArrayType.Length is null
            ? string.Empty
            : MarshalAs.UnmanagedLpArray(sizeParamIndex: ArrayType.Length.Value);

        protected internal ArrayRecordParameter(GirModel.Parameter parameter) : base(parameter)
        {
            parameter.AnyType.VerifyArrayType<GirModel.Record>();
        }
    }
}
