namespace Generator3.Model.Public
{
    public class StandardReturnType : ReturnType
    {
        public override string NullableTypeName => Model.AnyType.Match(
            type => type.GetName() + GetDefaultNullable(),
            arrayType => arrayType.GetName() //TODO: Consider if arrayType should be supported by this class
        );

        protected internal StandardReturnType(GirModel.ReturnType model) : base(model)
        {
        }
    }
}
