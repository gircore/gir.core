namespace Generator3.Generation.Model.Native
{
    public class StandardReturnType : ReturnType
    {
        public override string NullableTypeName => _returnValue.Type.GetName() + GetDefaultNullable();

        protected internal StandardReturnType(GirModel.ReturnType returnValue) : base(returnValue) { }
    }
}
