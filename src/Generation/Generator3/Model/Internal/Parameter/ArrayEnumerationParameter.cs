using Generator3.Converter;

namespace Generator3.Model.Internal
{
    public class ArrayEnumerationParameter : Parameter
    {
        private GirModel.ArrayType ArrayType => Model.AnyTypeReference.AnyType.AsT1;

        private GirModel.Enumeration Type => (GirModel.Enumeration) ArrayType.AnyTypeReference.AnyType.AsT0;

        public override string NullableTypeName => ArrayType.Length is null
            ? TypeNameConverter.Pointer
            : Type.GetFullyQualified() + "[]";

        public override string Direction => Model.GetDirection(
            @in: ParameterDirection.In,
            @out: ParameterDirection.Out,
            @outCallerAllocates: ParameterDirection.Ref,
            @inout: ParameterDirection.Ref
        );

        protected internal ArrayEnumerationParameter(GirModel.Parameter parameter) : base(parameter)
        {
            parameter.AnyTypeReference.AnyType.VerifyArrayType<GirModel.Enumeration>();
        }
    }
}
