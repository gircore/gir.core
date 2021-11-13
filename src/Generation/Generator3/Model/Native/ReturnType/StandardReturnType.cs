namespace Generator3.Model.Native
{
    //TODO: Consider Removing all "Standard" model classes as it is not clear in which cases they are used explicitly and replace them with concrete implementations
    
    public class StandardReturnType : ReturnType
    {
        public override string NullableTypeName => Model.AnyType.Match(
            type => type.GetName() + GetDefaultNullable(),
            arrayType => arrayType.GetName() //TODO: Consider if arrayType should be supported by this class
        );

        protected internal StandardReturnType(GirModel.ReturnType returnValue) : base(returnValue) { }
    }
}
