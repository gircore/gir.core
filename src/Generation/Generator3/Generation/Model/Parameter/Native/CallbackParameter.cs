namespace Generator3.Generation.Model.Native
{
    public class CallbackParameter : Parameter
    {
        //Native callbacks are not nullable
        public override string NullableTypeName => Model.Type.GetName();

        public override string Direction => ParameterDirection.In;

        protected internal CallbackParameter(GirModel.Parameter parameter) : base(parameter) { }
    }
}
