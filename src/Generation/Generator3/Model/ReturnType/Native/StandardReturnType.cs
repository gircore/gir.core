namespace Generator3.Model.Native
{
    public class StandardReturnType : ReturnType
    {
        public override string NullableTypeName => _returnValue.AnyType.Match(
            type => type.GetName() + GetDefaultNullable(),
            arrayType => arrayType.GetName() //TODO: Consider if arrayType should be supported by this class
        );

        protected internal StandardReturnType(GirModel.ReturnType returnValue) : base(returnValue) { }
    }
}
