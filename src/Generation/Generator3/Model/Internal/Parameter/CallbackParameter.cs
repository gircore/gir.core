namespace Generator3.Model.Internal
{
    public class CallbackParameter : Parameter
    {
        private GirModel.Callback Type => (GirModel.Callback) Model.AnyTypeReference.AnyType.AsT0;
        //Internal callbacks are not nullable
        public override string NullableTypeName => Type.Namespace.GetInternalName() + "." + Type.GetName();

        protected internal CallbackParameter(GirModel.Parameter parameter) : base(parameter)
        {
            parameter.AnyTypeReference.AnyType.VerifyType<GirModel.Callback>();
        }
    }
}
