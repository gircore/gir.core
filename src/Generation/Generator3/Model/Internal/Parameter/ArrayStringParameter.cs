using Generator3.Converter;

namespace Generator3.Model.Internal
{
    public class ArrayStringParameter : Parameter
    {
        private GirModel.ArrayType ArrayType => Model.AnyType.AsT1;

        public override string NullableTypeName
            => Model.Transfer == GirModel.Transfer.None && ArrayType.Length == null
                ? TypeNameExtension.Pointer // Arrays of string which do not transfer ownership and have no length index can not be marshalled automatically
                : ArrayType.GetName();

        public override string Attribute => ArrayType.Length is null
            ? string.Empty
            : MarshalAs.UnmanagedLpArray(sizeParamIndex: ArrayType.Length.Value);

        protected internal ArrayStringParameter(GirModel.Parameter parameter) : base(parameter)
        {
            parameter.AnyType.VerifyArrayType<GirModel.String>();
        }
    }
}
