using Generator3.Converter;

namespace Generator3.Model.Internal
{
    public class ArrayStringParameterForMethod : Parameter
    {
        private GirModel.ArrayType ArrayType => Model.AnyType.AsT1;

        public override string NullableTypeName
            => Model.Transfer == GirModel.Transfer.None && ArrayType.Length == null
                ? TypeNameConverter.Pointer // Arrays of string which do not transfer ownership and have no length index can not be marshalled automatically
                : ArrayType.GetName();

        public override string Attribute => ArrayType.Length is null
            ? string.Empty
            //We add 1 to the length because Methods contain an instance parameter which is not counted
            : MarshalAs.UnmanagedLpArray(ArrayType.Length.Value + 1);

        protected internal ArrayStringParameterForMethod(GirModel.Parameter parameter) : base(parameter)
        {
            parameter.AnyType.VerifyArrayType<GirModel.String>();
        }
    }
}
