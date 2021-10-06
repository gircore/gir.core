namespace Generator3.Generation.Code.Native
{
    public class CallbackParameter : Parameter
    {
        //Native callbacks are not nullable
        public override string NullableTypeName => _parameter.Type.GetName();

        public override string Direction => ParameterDirection.In;
        
        public CallbackParameter(GirModel.Parameter parameter) : base(parameter) { }
    }
}
