using Generator3.Converter;

namespace Generator3.Model.Internal
{
    public class ArrayPointerRecordParameter : Parameter
    {
        private GirModel.ArrayType ArrayType => Model.AnyType.AsT1;

        public override string NullableTypeName => TypeNameExtension.PointerArray;

        public override string Attribute => ArrayType.Length is null
            ? string.Empty
            : MarshalAs.UnmanagedLpArray(sizeParamIndex: ArrayType.Length.Value);

        protected internal ArrayPointerRecordParameter(GirModel.Parameter parameter) : base(parameter)
        {
            parameter.AnyType.VerifyArrayType<GirModel.Record>();
        }
    }
}
