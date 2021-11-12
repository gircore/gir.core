namespace Generator3.Model.Native
{
    public class CallbackParameter : Parameter
    {
        private GirModel.Callback Type => (GirModel.Callback) Model.AnyType.AsT0;
        //Native callbacks are not nullable
        public override string NullableTypeName => Type.Namespace.GetNativeName() + "." + Type.GetName();

        protected internal CallbackParameter(GirModel.Parameter parameter) : base(parameter)
        {
            parameter.AnyType.VerifyType<GirModel.Callback>();
        }
    }
}
