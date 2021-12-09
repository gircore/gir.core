using Generator3.Converter;

namespace Generator3.Model.Internal
{
    public class ArrayPointerRecordParameter : Parameter
    {
        private GirModel.ArrayType ArrayType => Model.AnyTypeReference.AnyType.AsT1;

        public override string NullableTypeName => TypeNameConverter.PointerArray;

        public override string Attribute => ArrayType.Length is null
            ? string.Empty
            : MarshalAs.UnmanagedLpArray(sizeParamIndex: ArrayType.Length.Value);

        protected internal ArrayPointerRecordParameter(GirModel.Parameter parameter) : base(parameter)
        {
            parameter.AnyTypeReference.AnyType.VerifyArrayType<GirModel.Record>();
        }
    }
}
