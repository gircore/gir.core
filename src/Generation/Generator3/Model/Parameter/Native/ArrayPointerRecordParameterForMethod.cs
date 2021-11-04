namespace Generator3.Model.Native
{
    public class ArrayPointerRecordParameterForMethod : Parameter
    {
        public override string NullableTypeName => "IntPtr[]";

        public override string Attribute => Model.AnyType.AsT1.Length is null
            ? string.Empty
            //We add 1 to the length because Methods contain an instance parameter which is not counted
            : $"[MarshalAs(UnmanagedType.LPArray, SizeParamIndex={Model.AnyType.AsT1.Length + 1})]";

        public override string Direction => Model.GetDirection(
            @in: ParameterDirection.In,
            @out: ParameterDirection.Out,
            @outCallerAllocates: ParameterDirection.Ref,
            @inout: ParameterDirection.Ref
        );

        protected internal ArrayPointerRecordParameterForMethod(GirModel.Parameter parameter) : base(parameter)
        {
            parameter.AnyType.VerifyArrayType<GirModel.Record>();
        }
    }
}
