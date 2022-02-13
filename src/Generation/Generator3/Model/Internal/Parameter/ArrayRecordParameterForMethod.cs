using Generator3.Converter;

namespace Generator3.Model.Internal
{
    public class ArrayRecordParameterForMethod : Parameter
    {
        private GirModel.ArrayType ArrayType => Model.AnyType.AsT1;

        public override string NullableTypeName => ArrayType.Length is null
            ? TypeNameExtension.PointerArray
            : ((GirModel.Record) ArrayType.AnyType.AsT0).GetFullyQualifiedInternalStructName() + "[]";

        public override string Attribute => ArrayType.Length is null
            ? string.Empty
            //We add 1 to the length because Methods contain an instance parameter which is not counted
            : MarshalAs.UnmanagedLpArray(sizeParamIndex: ArrayType.Length.Value + 1);

        protected internal ArrayRecordParameterForMethod(GirModel.Parameter parameter) : base(parameter)
        {
            parameter.AnyType.VerifyArrayType<GirModel.Record>();
        }
    }
}
