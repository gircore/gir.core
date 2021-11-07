namespace Generator3.Model.Native
{
    public class VoidParameter : Parameter
    {
        public override string NullableTypeName => "IntPtr";
        public override string Direction => "";

        protected internal VoidParameter(GirModel.Parameter parameter) : base(parameter)
        {
            parameter.AnyType.VerifyType<GirModel.Void>();
        }
    }
}
