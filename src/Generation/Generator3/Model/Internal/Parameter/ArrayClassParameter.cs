using Generator3.Converter;

namespace Generator3.Model.Internal
{
    public class ArrayClassParameter : Parameter
    {
        private GirModel.ArrayType ArrayType => Model.AnyType.AsT1;

        //Classes are always passed as a pointer. 
        public override string NullableTypeName => ArrayType.Length is null
            ? TypeNameExtension.Pointer
            : TypeNameExtension.PointerArray;

        public override string Attribute => ArrayType.Length is null
            ? string.Empty
            : MarshalAs.UnmanagedLpArray(sizeParamIndex: ArrayType.Length.Value);

        protected internal ArrayClassParameter(GirModel.Parameter parameter) : base(parameter)
        {
            parameter.AnyType.VerifyArrayType<GirModel.Class>();
        }
    }
}
