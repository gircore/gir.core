using Generator3.Converter;

namespace Generator3.Model.Internal
{
    public class ArrayClassParameterForMethods : Parameter
    {
        private GirModel.ArrayType ArrayType => Model.AnyTypeReference.AnyType.AsT1;

        //Classes are always passed as a pointer. 
        public override string NullableTypeName => ArrayType.Length is null
            ? TypeNameConverter.Pointer
            : TypeNameConverter.PointerArray;

        public override string Attribute => ArrayType.Length is null
            ? string.Empty
            : MarshalAs.UnmanagedLpArray(sizeParamIndex: ArrayType.Length.Value + 1);

        protected internal ArrayClassParameterForMethods(GirModel.Parameter parameter) : base(parameter)
        {
            parameter.AnyTypeReference.AnyType.VerifyArrayType<GirModel.Class>();
        }
    }
}
