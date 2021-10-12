namespace Generator3.Generation.Model.Native
{
    public class StandardReturnValue : ReturnValue
    {
        public override string NullableTypeName => _returnValue.Type.GetName() + GetDefaultNullable();

        protected internal StandardReturnValue(GirModel.ReturnValue returnValue) : base(returnValue) { }
    }
}
